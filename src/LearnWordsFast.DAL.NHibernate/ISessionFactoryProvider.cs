using NHibernate;

namespace LearnWordsFast.DAL.NHibernate
{
    public interface ISessionFactoryProvider
    {
        ISessionFactory Get();
    }
}