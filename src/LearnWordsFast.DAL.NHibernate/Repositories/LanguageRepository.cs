using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace LearnWordsFast.DAL.NHibernate.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly ISession _session;

        public LanguageRepository(ISession session)
        {
            _session = session;
        }

        public IList<Language> GetAll()
        {
            return _session.CreateCriteria<Language>().List<Language>();
        }

        public Language Get(Guid id)
        {
            return _session.Query<Language>().FirstOrDefault(x => x.Id == id);
        }

        public void Add(Language language)
        {
            _session.Save(language);
        }
    }
}