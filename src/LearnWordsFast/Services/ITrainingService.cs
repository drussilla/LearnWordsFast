using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.Services
{
    public interface ITrainingService
    { 
        Word GetNextWord();

        void FinishTraining(Word word);
    }
}