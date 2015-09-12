using System;
using System.Collections.Generic;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;

namespace LearnWordsFast.DAL.NHibernate.Repositories
{
    public class WordNHibernateRepository : IWordRepository
    {
        private readonly ISessionProvider _sessionProvider;

        public WordNHibernateRepository(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public void Add(Word word)
        {
            _sessionProvider.GetSession().SaveOrUpdate(word);
        }

        public Word Get(Guid id)
        {
            return _sessionProvider.GetSession().Get<Word>(id);
        }

        public IList<Word> GetAll()
        {
            return _sessionProvider.GetSession().CreateCriteria<Word>().List<Word>();
        }

        public IList<Word> GetLastTrainedBefore(DateTime date)
        {
            return _sessionProvider.GetSession().CreateCriteria<Word>().List<Word>();
        }

        public void Update(Word word)
        {
            _sessionProvider.GetSession().SaveOrUpdate(word);
        }
    }
}