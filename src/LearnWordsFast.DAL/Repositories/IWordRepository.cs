using System;
using System.Collections.Generic;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.DAL.Repositories
{
    public interface IWordRepository
    {
        void Add(Word word);
        Word Get(Guid id, Guid userId);
        IList<Word> GetAll(Guid userId);
        IList<Word> GetLastTrainedBefore(DateTime date, Guid userId);
        void Update(Word word);
        void Delete(Word word);
    }
}