using System;

namespace LearnWordsFast.DAL.EF
{
    public class RequestDbContext : IDisposable
    {
        private readonly Lazy<Context> _context;
         
        public RequestDbContext(Lazy<Context> context)
        {
            _context = context;
        }

        public void Dispose()
        {
            if (_context.IsValueCreated && _context.Value != null)
            {
                _context.Value.SaveChanges();
                _context.Value.Dispose();
            }
        }
    }
}