using FluentNHibernate.Mapping;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.DAL.NHibernate.ModelMappings
{
    public class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {
            Id(x => x.Id)
                .GeneratedBy
                .Assigned();
            Map(x => x.Email).Unique();
            Map(x => x.Password);

            References(x => x.TrainingLanguage)
                .ForeignKey("training_language");
            References(x => x.MainLanguage)
                .ForeignKey("main_language");

            HasManyToMany(x => x.AdditionalLanguages)
                .Table("UsersAdditionalLanguages")
                .ForeignKeyConstraintNames("user_ref", "language");

            HasMany(x => x.Words)
                .ForeignKeyConstraintName("word");

            Table("Users");
        }
    }
}