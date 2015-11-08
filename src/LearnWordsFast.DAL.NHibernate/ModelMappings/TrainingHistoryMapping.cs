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
            Map(x => x.WordId, "word_id");

            References(x => x.Word, "word_id")
                .ForeignKey("word_to_history");

            Table("TrainingHistory");
        }
    }
}