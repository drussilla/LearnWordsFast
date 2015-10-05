using FluentNHibernate.Mapping;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.DAL.NHibernate.ModelMappings
{
    public class AdditionalTranslationMapping : ClassMap<AdditionalTranslation>
    {
        public AdditionalTranslationMapping()
        {
            Id(x => x.Id)
               .GeneratedBy
               .Assigned();

            Map(x => x.Translation);

            References(x => x.Word).ForeignKey("word");
            References(x => x.Language).ForeignKey("language");

            Table("AdditionalTranslations");
        }
    }
}