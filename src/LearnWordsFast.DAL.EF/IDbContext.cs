using System;

namespace LearnWordsFast.DAL.EF
{
    public interface IDbContext
    {
        void SetContext(Lazy<Context> session);

        Context Current { get; }
    }
}