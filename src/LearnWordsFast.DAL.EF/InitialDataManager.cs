using LearnWordsFast.DAL.InitialData;
using LearnWordsFast.DAL.Repositories;

namespace LearnWordsFast.DAL.EF
{
    public class InitialDataManager : InitializeDataManager
    {
        public InitialDataManager(ILanguageRepository languageRepository) : base(languageRepository)
        {
        }
    }
}