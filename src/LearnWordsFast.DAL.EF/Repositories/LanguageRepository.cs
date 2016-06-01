using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;

namespace LearnWordsFast.DAL.EF.Repositories
{
    public class LanguageRepository :ILanguageRepository
    {
        private readonly Context _db;

        public LanguageRepository(Context db)
        {
            _db = db;
        }

        public IList<Language> GetAll()
        {
            return _db.Languages.ToList();
        }

        public Language Get(Guid id)
        {
            return _db.Languages.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Language language)
        {
            _db.Languages.Add(language);
        }

        public void Update(Language language)
        {
            _db.Languages.Update(language);
        }

        public void AddOrUpdate(Language language)
        {
            var existing = _db.Languages.FirstOrDefault(x => x.Id == language.Id);
            if (existing == null)
            {
                _db.Languages.Add(language);
            }
                else
                {
                    existing.Name = language.Name;
                _db.Languages.Update(existing);
                }

            _db.SaveChanges();
        }
    }
}