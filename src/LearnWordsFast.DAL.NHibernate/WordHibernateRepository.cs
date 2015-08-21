using System;
using System.Collections.Generic;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using NHibernate;

namespace LearnWordsFast.DAL.NHibernate
{
    public class WordHibernateRepository : IWordRepository
    {
        private readonly ISessionProvider sessionProvider;

        public WordHibernateRepository(ISessionProvider sessionProvider)
        {
            this.sessionProvider = sessionProvider;
        }

        public void Add(Word word)
        {
            sessionProvider.GetSession().SaveOrUpdate(word);
        }

        public Word Get(Guid id)
        {
            return sessionProvider.GetSession().Get<Word>(id);
        }

        public IList<Word> GetAll()
        {
            return sessionProvider.GetSession().CreateCriteria<Word>().List<Word>();
        }

        public IList<Word> GetLastTrainedBefore(DateTime date)
        {
            return sessionProvider.GetSession().CreateCriteria<Word>().List<Word>();
        }

        public void Update(Word word)
        {
            sessionProvider.GetSession().SaveOrUpdate(word);
        }
    }
}