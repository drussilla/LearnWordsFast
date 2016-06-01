using System;
using LearnWordsFast.DAL.InitialData;
using LearnWordsFast.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearnWordsFast.DAL.EF
{
    public class InitialDataManager : InitializeDataManager
    {
        private readonly Context _db;

        public InitialDataManager(ILanguageRepository languageRepository, Context db)
            : base(languageRepository)
        {
            _db = db;
        }

        public override void Initialize()
        {
            _db.Database.Migrate();
            base.Initialize();
        }
    }
}