using System;
using NHibernate;

namespace LearnWordsFast.DAL.NHibernate
{
    public interface ISessionManager
    {
        Lazy<ISession> OpenSession(ISessionFactory sessionFactory, ISessionProvider sessionProvider);
        void CloseSession(Lazy<ISession> session);
    }
}