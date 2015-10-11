﻿using FluentNHibernate.Mapping;
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
            References(x => x.Language)
                .ForeignKey("language");
            Map(x => x.Original);
            Map(x => x.TrainingAmout);
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

            Table("Words");
        }
    }
}