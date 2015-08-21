using System;
using System.Collections.Generic;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;

namespace LearnWordsFast.DAL.NHibernate
{
    public class WordHibernateRepository : IWordRepository
    {
        public void Add(Word word)
        {
            throw new NotImplementedException();
        }

        public Word Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Word> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Word> GetLastTrainedBefore(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void Update(Word word)
        {
            throw new NotImplementedException();
        }
    }
}