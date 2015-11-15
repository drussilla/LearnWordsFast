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

            Map(x => x.AddedDateTime);
            References(x => x.Language)
                .ForeignKey("language");
            Map(x => x.Original);
            
            References(x => x.Translation)
                .Cascade.All()
                .ForeignKey("mainTranslation");
            Map(x => x.Context);
            Map(x => x.UserId).Column("user_id");

            HasManyToMany(x => x.AdditionalTranslations)
                .Cascade.All()
                .ForeignKeyConstraintNames("word", "translation")
                .Table("wordAdditionalTranslations")
                .Cascade.All();

            HasMany(x => x.TrainingHistories)
                .Cascade.AllDeleteOrphan();

            Table("Words");
        }
    }
}