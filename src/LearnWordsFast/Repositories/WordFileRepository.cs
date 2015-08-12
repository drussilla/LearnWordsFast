using System;
using System.Collections.Generic;
using System.IO;
using LearnWordsFast.Models;
using Microsoft.Framework.Configuration;

namespace LearnWordsFast.Repositories
{
    public class WordFileRepository : IWordRepository
    {
        private readonly IConfiguration config;
        private readonly string fileLocation;

        public WordFileRepository(IConfiguration config)
        {
            this.config = config;
            fileLocation = config.Get("Data:WordFilePath");
        }

        public void Add(Word word)
        {
            throw new NotImplementedException();
        }

        public Word Get(string word)
        {
            throw new NotImplementedException();
        }

        public List<Word> GetLastTrainedBefore(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}