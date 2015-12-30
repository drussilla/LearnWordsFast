using LearnWordsFast.DAL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LearnWordsFast.API.ViewModels.TrainingController
{
    public abstract class TrainingViewModel
    {
        protected TrainingViewModel(TrainingTypeModel type)
        {
            Type = type;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public TrainingType Type { get; private set; }
    }
}