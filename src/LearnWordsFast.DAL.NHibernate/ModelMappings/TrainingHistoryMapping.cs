using FluentNHibernate.Mapping;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.DAL.NHibernate.ModelMappings
{
    public class TrainingHistoryMapping : ClassMap<TrainingHistory>
    {
        public TrainingHistoryMapping()
        {
            Id(x => x.Id)
                .GeneratedBy
                .Assigned();

            Map(x => x.IsCorrect);
            Map(x => x.Score);
            Map(x => x.Date);
            Map(x => x.Type).CustomType<TrainingType>();
            
            Table("TrainingHistory");
        }
    }
}