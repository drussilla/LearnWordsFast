using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using Microsoft.Framework.Configuration;

namespace LearnWordsFast.Repositories
{
    public class WordFileRepository : IWordRepository
    {
        private readonly string fileLocation;
        private readonly XmlSerializer serializer = new XmlSerializer(typeof(List<Word>));
        private readonly IList<Word> words; 

        public WordFileRepository(IConfiguration config)
        {
            var fileName = config.Get("Data:WordFilePath");
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var directory = Path.Combine(appData, "LearnWordsFast");
            fileLocation =  Path.Combine(directory, fileName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            words = LoadAll();
        }

        private IList<Word> LoadAll()
        {
            if (!File.Exists(fileLocation))
            {
                return new List<Word>();
            }

            using (var file = File.Open(fileLocation, FileMode.Open))
            {
                return serializer.Deserialize(file) as IList<Word>;
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

        public Word Get(Guid id)
        {
            return words
                .FirstOrDefault(x => x.Id == id);
        }

        public IList<Word> GetAll()
        {
            return words;
        }

        public IList<Word> GetLastTrainedBefore(DateTime date)
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