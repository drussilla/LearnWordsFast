using System;
using System.Collections.Generic;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.DAL.Repositories
{
    public interface ILanguageRepository
    {
        IList<Language> GetAll();

        Language Get(Guid id);

        void Add(Language language);

        void Update(Language language);

        void AddOrUpdate(Language language);
    }
}