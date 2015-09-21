using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LearnWordsFast.DAL.InitialData;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using NHibernate;

namespace LearnWordsFast.DAL.NHibernate
{
    public class InitialDataManager : IInitializeDataManager
    {
        private readonly ISessionManager _sessionManager;
        private readonly ISessionFactory _sessionFactory;
        private readonly ISessionProvider _sessionProvider;
        private readonly ILanguageRepository _languageRepository;

        public InitialDataManager(ISessionManager sessionManager, ISessionFactory sessionFactory, ISessionProvider sessionProvider, ILanguageRepository languageRepository)
        {
            _sessionManager = sessionManager;
            _sessionFactory = sessionFactory;
            _sessionProvider = sessionProvider;
            _languageRepository = languageRepository;
        }

        public void Initialize()
        {
            var session = _sessionManager.OpenSession(_sessionFactory, _sessionProvider);
            var serializer = new XmlSerializer(typeof(List<Language>));
            using (var file = File.OpenRead("D:\\_tmp\\lang.xml"))
            {
                var list = serializer.Deserialize(file) as List<Language>;
                foreach (var language in list)
                {
                    if (_languageRepository.Get(language.Id) == null)
                    {
                        _languageRepository.Add(language);
                    }
                }
            }

            _sessionManager.CloseSession(session);
        }
    }
}