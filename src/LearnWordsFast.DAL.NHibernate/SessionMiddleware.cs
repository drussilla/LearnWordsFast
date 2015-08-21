using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using NHibernate;

namespace LearnWordsFast.DAL.NHibernate
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate request;
        
        public SessionMiddleware(RequestDelegate request)
        {
            this.request = request;
        }

        public async Task Invoke(HttpContext context)
        {
            var session = new Lazy<ISession>(() =>
            {
                var sessionFactory = context.ApplicationServices.GetService(typeof(ISessionFactory)) as ISessionFactory;
                if (sessionFactory == null)
                {
                    throw new Exception("ISessionFactory is not registered. Please register it with AddNHibernateSession() method");
                }

                var currentSession = sessionFactory.OpenSession();
                currentSession.BeginTransaction();
                return currentSession;
            });

            var currentRequestSessionProvider = context.RequestServices.GetService(typeof (ISessionProvider)) as ISessionProvider;
            if (currentRequestSessionProvider == null)
            {
                throw new Exception("ISessionProvider is not registered. Please register it with AddNHibernateSession() method");
            }

            currentRequestSessionProvider.SetSession(session);

            await request(context);

            if (session.IsValueCreated && session.Value != null)
            {
                if (session.Value.Transaction != null && session.Value.Transaction.IsActive)
                {
                    session.Value.Transaction.Commit();
                }

                session.Value.Dispose();
            }
        }
    }
}