using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using LearnWordsFast.Models;
using Microsoft.Framework.Configuration;

namespace LearnWordsFast.Repositories
{
    public class WordFileRepository : IWordRepository
    {
        private readonly IConfiguration config;
        private readonly string fileLocation;
        private readonly XmlSerializer serializer = new XmlSerializer(typeof(List<Word>));
        private readonly List<Word> words; 

        public WordFileRepository(IConfiguration config)
        {
            this.config = config;
            fileLocation = config.Get("Data:WordFilePath");
            words = LoadAll();
        }

        private List<Word> LoadAll()
        {
            if (!File.Exists(fileLocation))
            {
                return new List<Word>();
            }

            using (var file = File.Open(fileLocation, FileMode.Open))
            {
                return serializer.Deserialize(file) as List<Word>;
            }
        }

        public void Add(Word word)
        {
            words.Add(word);
            SaveAll();
        }

        private void SaveAll()
        {
            using (var file = File.Open(fileLocation, FileMode.Create))
            {
                serializer.Serialize(file, words);
            }
        }

        public Word Get(string word)
        {
            return words
                .FirstOrDefault(x => x.Original.Equals(word, StringComparison.OrdinalIgnoreCase));
        }

        public List<Word> GetAll()
        {
            return words;
        }

        public List<Word> GetLastTrainedBefore(DateTime date)
        {
            return words
                .Where(x => x.LastTrainingDateTime == null || x.LastTrainingDateTime < date)
                .ToList();
        }

        public void Update(Word word)
        {
            var existingWord = words.FirstOrDefault(x => x.Id == word.Id);
            if (existingWord == null)
            {
                Add(word);
                return;
            }

            if (existingWord == word)
            {
                SaveAll();
                return;
            }

            words.Remove(existingWord);
            Add(word);
        }
    }
}