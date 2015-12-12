using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using Microsoft.Data.Entity;

namespace LearnWordsFast.DAL.EF.Repositories
{
    public class WordRepository : IWordRepository
    {
        public void Add(Word word)
        {
            using (var db = new Context())
            {
                db.Translations.Add(word.Translation);
                foreach (var wordAdditionalTranslation in word.AdditionalTranslations)
                {
                    db.Translations.Add(wordAdditionalTranslation.Translation);
                }

                db.Words.Add(word);
                db.SaveChanges();
            }
        }

        public Word Get(Guid id, Guid userId)
        {
            using (var db = new Context())
            {
                return db.Words
                    .Include(x => x.Language)
                    .Include(x => x.Translation)
                    .Include(x => x.TrainingHistories)
                    .Include(x => x.AdditionalTranslations)
                    .ThenInclude(x => x.Translation)
                    .SingleOrDefault(x => x.Id == id && x.UserId == userId);
            }
        }

        public IList<Word> GetAll(Guid userId)
        {
            using (var db = new Context())
            {
                return db.Words
                    .Where(x => x.UserId == userId)
                    .Include(x => x.Language)
                    .Include(x => x.Translation)
                    .Include(x => x.TrainingHistories)
                    .Include(x => x.AdditionalTranslations)
                    .ThenInclude(x => x.Translation)
                    .ToList();
            }
        }

        public IList<Word> GetLastTrainedBefore(DateTime date, Guid userId)
        {
            using (var db = new Context())
            {
                return db.Words
                    .Where(x => x.UserId == userId)
                    .ToList();
            }
        }

        public void Update(Word word)
        {
            using (var db = new Context())
            {
                db.Words.Update(word);
                db.SaveChanges();
            }
        }

        public void Delete(Word word)
        {
            using (var db = new Context())
            {
                db.Words.Remove(word);
                db.SaveChanges();
            }
        }
    }
}