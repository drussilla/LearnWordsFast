namespace LearnWordsFast.DAL.Models
{
    public abstract class TrainingTypeModel
    {
        protected TrainingTypeModel(TrainingType type)
        {
            Type = type;
        }

        public TrainingType Type { get; }

        public static implicit operator TrainingType(TrainingTypeModel model)
        {
            return model.Type;
        }
    }

    public class OneRight : TrainingTypeModel
    {
        public readonly static OneRight RepeatTranslation = new OneRight(TrainingType.RepeatTranslation);
        public readonly static OneRight TypeOriginal = new OneRight(TrainingType.TypeOriginal);
        public readonly static OneRight TypeTranslation = new OneRight(TrainingType.TypeTranslation);
        public readonly static OneRight ComposeOriginal = new OneRight(TrainingType.ComposeOriginal);

        protected internal OneRight(TrainingType type) : base(type)
        {
        }
    }

    public class OneRightManyWrong : TrainingTypeModel
    {
        public readonly static OneRightManyWrong ChooseOriginal = new OneRightManyWrong(TrainingType.ChooseOriginal);
        public readonly static OneRightManyWrong ChooseTranslation = new OneRightManyWrong(TrainingType.ChooseTranslation);

        private OneRightManyWrong(TrainingType type) : base(type)
        {
        }
    }

    public class ManyRight : TrainingTypeModel
    {
        public readonly static ManyRight MatchWords = new ManyRight(TrainingType.MatchWords);

        private ManyRight(TrainingType type) : base(type)
        {
        }
    }

    public class NoWords : TrainingTypeModel
    {

        public readonly static NoWords Type = new NoWords();
        private NoWords() : base(TrainingType.NoWords)
        {
        }
    }
}