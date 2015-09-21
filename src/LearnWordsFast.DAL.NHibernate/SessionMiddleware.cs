using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using NHibernate;

namespace LearnWordsFast.DAL.NHibernate
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _request;
        
        public SessionMiddleware(RequestDelegate request)
        {
            this._request = request;
        }

        public async Task Invoke(HttpContext context)
        {
            var sessionManager = context.ApplicationServices.GetService(typeof(ISessionManager)) as ISessionManager;

            if (sessionManager == null)
            {
                throw new Exception("ISessionManager is not registered. Please register it with AddNHibernateSession() method");
            }

            var sessionFactory = context.ApplicationServices.GetService(typeof(ISessionFactory)) as ISessionFactory;
            var sessionProvider = context.RequestServices.GetService(typeof(ISessionProvider)) as ISessionProvider;
            var session = sessionManager.OpenSession(sessionFactory, sessionProvider);

            await _request(context);

            sessionManager.CloseSession(session);
        }
    }
}