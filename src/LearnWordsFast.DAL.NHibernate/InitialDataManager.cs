using LearnWordsFast.DAL.InitialData;
using LearnWordsFast.DAL.Repositories;
using NHibernate;

namespace LearnWordsFast.DAL.NHibernate
{
    public class InitialDataManager : InitializeDataManager
    {
        private readonly ISessionManager _sessionManager;
        private readonly ISessionFactory _sessionFactory;
        private readonly ISessionProvider _sessionProvider;
        
        public InitialDataManager(ISessionManager sessionManager, ISessionFactory sessionFactory, ISessionProvider sessionProvider, ILanguageRepository languageRepository):
            base(languageRepository)
        {
            _sessionManager = sessionManager;
            _sessionFactory = sessionFactory;
            _sessionProvider = sessionProvider;
        }

        public override void Initialize()
        {
            var session = _sessionManager.OpenSession(_sessionFactory, _sessionProvider);
            
            base.Initialize();

            _sessionManager.CloseSession(session);
        }
    }
}