using System;
using System.Collections.Generic;
using LearnWordsFast.Models;

namespace LearnWordsFast.Repositories
{
    public interface IWordRepository
    {
        void Add(Word word);
        Word Get(string word);
        List<Word> GetAll();
        List<Word> GetLastTrainedBefore(DateTime date);
        void Update(Word word);
    }
}