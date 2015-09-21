using FluentNHibernate.Mapping;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.DAL.NHibernate.ModelMappings
{
    public class LanguageMapping : ClassMap<Language>
    {
        public LanguageMapping()
        {
            Id(x => x.Id)
                .GeneratedBy
                .Assigned();
            Map(x => x.Name).Unique();
            Table("Languages");
        }
    }
}