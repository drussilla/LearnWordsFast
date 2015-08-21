using System;
using NHibernate;

namespace LearnWordsFast.DAL.NHibernate
{
    public interface ISessionProvider
    {
        void SetSession(Lazy<ISession> session);
        ISession GetSession();
    }
}