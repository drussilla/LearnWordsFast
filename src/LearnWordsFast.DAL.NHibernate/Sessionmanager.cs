using System;
using NHibernate;

namespace LearnWordsFast.DAL.NHibernate
{
    public class SessionManager : ISessionManager
    {
        public Lazy<ISession> OpenSession(ISessionFactory sessionFactory, ISessionProvider sessionProvider)
        {
            var session = new Lazy<ISession>(() =>
            {
                if (sessionFactory == null)
                {
                    throw new Exception("ISessionFactory is not registered. Please register it with AddNHibernateSession() method");
                }

                var currentSession = sessionFactory.OpenSession();
                currentSession.BeginTransaction();
                return currentSession;
            });

            if (sessionProvider == null)
            {
                throw new Exception("ISessionProvider is not registered. Please register it with AddNHibernateSession() method");
            }

            sessionProvider.SetSession(session);
            return session;
        }

        public void CloseSession(Lazy<ISession> session)
        {
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