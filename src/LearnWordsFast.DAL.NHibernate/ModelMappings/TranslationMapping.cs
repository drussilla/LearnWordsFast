using FluentNHibernate.Mapping;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.DAL.NHibernate.ModelMappings
{
    public class TranslationMapping : ClassMap<Translation>
    {
        public TranslationMapping()
        {
            Id(x => x.Id)
               .GeneratedBy
               .Assigned();

            Map(x => x.TranslationText);

            References(x => x.Language).ForeignKey("language");

            Table("Translations");
        }
    }
}