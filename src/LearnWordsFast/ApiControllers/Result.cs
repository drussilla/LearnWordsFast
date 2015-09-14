namespace LearnWordsFast.ApiControllers
{
    public class Result
    {
        protected Result()
        {
        }

        public int Status { get; protected set; }

        public string ErrorMessage { get; protected set; }

        public static Result Ok()
        {
            return new Result { Status = 200 };
        }

        public static Result Error(string message)
        {
            return new Result { Status = 400, ErrorMessage = message};
        }
    }

    public class Result<T> : Result
    {
        private Result()
        {
        }

        public T Data { get; private set; }

        public static Result<T> Ok(T data)
        {
            return new Result<T> {Status = 200, Data = data};
        }
    }
}