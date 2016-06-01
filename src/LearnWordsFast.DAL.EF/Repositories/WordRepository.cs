using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearnWordsFast.DAL.EF.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly Context _db;

        public WordRepository(Context db)
        {
            _db = db;
        }

        public void Add(Word word)
        {

            _db.Translations.Add(word.Translation);
            foreach (var wordAdditionalTranslation in word.AdditionalTranslations)
            {
                _db.Translations.Add(wordAdditionalTranslation.Translation);
            }

            _db.Words.Add(word);

        }

        public Word Get(Guid id, Guid userId)
        {
            
            return _db.Words
                .Include(x => x.Language)
                .Include(x => x.Translation)
                .Include(x => x.TrainingHistories)
                .Include(x => x.AdditionalTranslations)
                .ThenInclude(x => x.Translation)
                .SingleOrDefault(x => x.Id == id && x.UserId == userId);
        }

        public IList<Word> GetAll(Guid userId)
        {
            return _db.Words
                .Where(x => x.UserId == userId)
                .Include(x => x.Language)
                .Include(x => x.Translation)
                .Include(x => x.TrainingHistories)
                .Include(x => x.AdditionalTranslations)
                .ThenInclude(x => x.Translation)
                .ToList();
        }

        public IList<Word> GetLastTrainedBefore(DateTime date, Guid userId)
        {
            return _db.Words
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public void Update(Word word)
        {
        }

        public void Delete(Word word)
        {
            _db.Words.Remove(word);
        }
    }
}