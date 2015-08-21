using System;
using System.Collections.Generic;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.DAL.Repositories
{
    public interface IWordRepository
    {
        void Add(Word word);
        Word Get(Guid id);
        IList<Word> GetAll();
        IList<Word> GetLastTrainedBefore(DateTime date);
        void Update(Word word);
    }
}