using FluentNHibernate.Mapping;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.DAL.NHibernate.ModelMappings
{
    public class WordMapping  : ClassMap<Word>
    {
        public WordMapping()
        {
            Id(x => x.Id);
            Map(x => x.LastTrainingDateTime);
            Map(x => x.AddedDateTime);
            Map(x => x.Original);
            Map(x => x.TrainingAmout);
            Map(x => x.Translation);
            Table("Words");
        }
    }
}