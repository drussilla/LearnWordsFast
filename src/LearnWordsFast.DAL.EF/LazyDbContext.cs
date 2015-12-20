using System;

namespace LearnWordsFast.DAL.EF
{
    public class LazyDbContext : IDbContext
    {
        private Lazy<Context> _currentContext; 
        public void SetContext(Lazy<Context> currentContext)
        {
            _currentContext = currentContext;
        }

        public Context Current => _currentContext.Value;
    }
}