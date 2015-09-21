using System;
using System.Collections.Generic;
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
            
            InitializeLanguages();

            _sessionManager.CloseSession(session);
        }

        private void InitializeLanguages()
        {
            foreach (var language in Languages)
            {
                _languageRepository.AddOrUpdate(language);
            }
        }

        private static readonly List<Language> Languages = new List<Language>
        {
            new Language {Id = Guid.Parse("2376550e-dd88-44a7-98f8-e550c4456e8f"), Name = "English"},
            new Language {Id = Guid.Parse("dd351e4f-16f8-4c4a-80fd-9d4bdbf6aad4"), Name = "Ukrainian"},
            new Language {Id = Guid.Parse("01be86aa-779a-4792-b6a4-d5063a24de43"), Name = "Russian"},
            new Language {Id = Guid.Parse("a518fa58-94eb-458f-af5b-a492a2e9b9a0"), Name = "Dutch"},
            new Language {Id = Guid.Parse("f50beb52-368a-4b52-8463-bd02fb904267"), Name = "Spanish"},
            new Language {Id = Guid.Parse("d2cf1411-5d8f-4beb-b910-94cffd841336"), Name = "Portuguese"},
            new Language {Id = Guid.Parse("6e33fae9-8165-4674-9878-33ccb3baf33e"), Name = "German"},
            new Language {Id = Guid.Parse("adfe2646-876d-4e3c-a1a7-18da88d29cdf"), Name = "French"},
            new Language {Id = Guid.Parse("57dc2c4b-d06d-47ce-9cad-5a1ff2b42d54"), Name = "Italian"},
            new Language {Id = Guid.Parse("00a9fbe2-3f7d-4dc5-85e5-3a99530acc1c"), Name = "Polish"}
        };
    }
}