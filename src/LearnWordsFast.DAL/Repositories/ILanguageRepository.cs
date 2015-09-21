using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.DAL.Repositories
{
    public interface ILanguageRepository
    {
        IList<Language> GetAll();

        Language Get(Guid id);

        void Add(Language language);
    }
}