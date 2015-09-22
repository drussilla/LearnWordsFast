﻿using FluentNHibernate.Mapping;
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

            References(x => x.TrainingLanguage);
            References(x => x.MainLanguage);

            HasManyToMany(x => x.AdditionalLanguages)
                .Table("UsersAdditionalLanguages");

            Table("Users");
        }
    }
}