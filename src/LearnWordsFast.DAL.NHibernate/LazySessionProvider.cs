using System;
using NHibernate;

namespace LearnWordsFast.DAL.NHibernate
{
    public class LazySessionProvider : ISessionProvider
    {
        private Lazy<ISession> currentSession;

        public LazySessionProvider()
        {
            
        }
        public void SetSession(Lazy<ISession> session)
        {
            currentSession = session;
        }

        public ISession GetSession()
        {
            return currentSession.Value;
        }
    }
}