using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using NHibernate.Linq;

namespace LearnWordsFast.DAL.NHibernate.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly ISessionProvider _sessionProvider;

        public WordRepository(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public void Add(Word word)
        {
            var session = _sessionProvider.GetSession();
            if (word.AdditionalTranslations != null && word.AdditionalTranslations.Count > 0)
            {
                foreach (var additionalTranslation in word.AdditionalTranslations)
                {
                    session.Save(additionalTranslation);
                }
            }

            session.Save(word.Translation);
            session.SaveOrUpdate(word);
        }

        public Word Get(Guid id, Guid userId)
        {
            return _sessionProvider.GetSession()
                .Query<Word>()
                .FirstOrDefault(x => x.Id == id && x.UserId == userId);
        }

        public IList<Word> GetAll(Guid userId)
        {
            return _sessionProvider.GetSession()
                .Query<Word>()
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public IList<Word> GetLastTrainedBefore(DateTime date, Guid userId)
        {
            return _sessionProvider.GetSession()
                .Query<Word>()
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public void Update(Word word)
        {
            _sessionProvider.GetSession().SaveOrUpdate(word);
        }

        public void Delete(Word word)
        {
            var session = _sessionProvider.GetSession();
            session.Delete(word);
        }
    }
}