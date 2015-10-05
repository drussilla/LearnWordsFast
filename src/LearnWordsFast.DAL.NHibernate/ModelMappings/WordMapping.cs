using FluentNHibernate.Mapping;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.DAL.NHibernate.ModelMappings
{
    public class WordMapping  : ClassMap<Word>
    {
        public WordMapping()
        {
            Id(x => x.Id)
                .GeneratedBy
                .Assigned();
            Map(x => x.LastTrainingDateTime)
                .Nullable();
            Map(x => x.AddedDateTime);
            Map(x => x.Original);
            Map(x => x.TrainingAmout);
            Map(x => x.Translation);
            Map(x => x.Context);
            Map(x => x.UserId).Column("user_id");

            HasMany(x => x.AdditionalTranslations);

            Table("Words");
        }
    }
}