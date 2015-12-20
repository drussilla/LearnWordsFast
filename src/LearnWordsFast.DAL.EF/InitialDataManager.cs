using System;
using LearnWordsFast.DAL.InitialData;
using LearnWordsFast.DAL.Repositories;

namespace LearnWordsFast.DAL.EF
{
    public class InitialDataManager : InitializeDataManager
    {
        private readonly IDbContext _db;

        public InitialDataManager(ILanguageRepository languageRepository, IDbContext db) : base(languageRepository)
        {
            _db = db;
        }

        public override void Initialize()
        {
            var lazyContext = new Lazy<Context>(() => new Context());
            _db.SetContext(lazyContext);
            using (new RequestDbContext(lazyContext))
            {
                base.Initialize();
            }
        }
    }
}