using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;

namespace LearnWordsFast.DAL.EF.Repositories
{
    public class LanguageRepository :ILanguageRepository
    {
        public IList<Language> GetAll()
        {
            using (var db = new Context())
            {
                return db.Languages.ToList();
            }
        }

        public Language Get(Guid id)
        {
            using (var db = new Context())
            {
                return db.Languages.FirstOrDefault(x => x.Id == id);
            }
        }

        public void Add(Language language)
        {
            using (var db = new Context())
            {
                db.Languages.Add(language);
            }
        }

        public void Update(Language language)
        {
            using (var db = new Context())
            {
                db.Languages.Update(language);
            }
        }

        public void AddOrUpdate(Language language)
        {
            using (var db = new Context())
            {
                var existing = db.Languages.FirstOrDefault(x => x.Id == language.Id);
                if (existing == null)
                {
                    db.Languages.Add(language);
                }
                else
                {
                    existing.Name = language.Name;
                    db.Languages.Update(existing);
                }

                db.SaveChanges();
            }
        }
    }
}