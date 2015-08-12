using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using LearnWordsFast.Models;

namespace LearnWordsFast.Repositories
{
    public interface IWordRepository
    {
        void Add(Word word);
        Word Get(string word);
        List<Word> GetLastTrainedBefore(DateTime date);
    }
}