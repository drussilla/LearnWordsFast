using System.Collections.Generic;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using NHibernate;

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
    }
}