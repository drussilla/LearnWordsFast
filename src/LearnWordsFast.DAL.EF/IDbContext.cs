using System;
using Microsoft.Data.Entity;

namespace LearnWordsFast.DAL.EF
{
    public interface IDbContext
    {
        void SetContext(Lazy<Context> context);

        Context Current { get; }
    }
}