using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using NHibernate.Linq;

namespace LearnWordsFast.DAL.NHibernate.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly ISessionProvider _sessionProvider;

        public LanguageRepository(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public IList<Language> GetAll()
        {
            return _sessionProvider.GetSession().CreateCriteria<Language>().List<Language>();
        }

        public Language Get(Guid id)
        {
            return _sessionProvider.GetSession().Query<Language>().FirstOrDefault(x => x.Id == id);
        }

        public void Add(Language language)
        {
            _sessionProvider.GetSession().Save(language);
        }

        public void Update(Language language)
        {
            _sessionProvider.GetSession().Update(language);
        }

        public void AddOrUpdate(Language language)
        {
            _sessionProvider.GetSession().SaveOrUpdate(language);
        }
    }
}