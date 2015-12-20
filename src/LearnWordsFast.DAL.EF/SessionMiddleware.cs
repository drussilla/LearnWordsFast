using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace LearnWordsFast.DAL.EF
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _request;
        
        public SessionMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task Invoke(HttpContext context)
        {
            var contextProvider = context.RequestServices.GetService(typeof(IDbContext)) as IDbContext;
            if (contextProvider == null)
            {
                throw new Exception("IDbContext is not registered. Please register it with AddEF() method");
            }

            var lazyContext = new Lazy<Context>(() => new Context());
            contextProvider.SetContext(lazyContext);
            using (new RequestDbContext(lazyContext))
            {
                await _request(context);
            }
        }
    }
}