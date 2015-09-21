using System.Collections;
using System.Collections.Generic;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.DAL.Repositories
{
    public interface ILanguageRepository
    {
        IList<Language> GetAll();
    }
}