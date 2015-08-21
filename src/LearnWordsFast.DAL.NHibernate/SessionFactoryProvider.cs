using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace LearnWordsFast.DAL.NHibernate
{
    public class SessionFactoryProvider
    {
        public static ISessionFactory GetSessionFactory()
        {
            return Fluently
                .Configure()
                    .Database(
                        PostgreSQLConfiguration.Standard
                        .ConnectionString(c =>
                            c.Host("localhost")
                            .Port(5432)
                            .Database("mydb")
                            .Username("postgres")
                            .Password("password!")))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ModelMappings.WordMapping>())
                    .ExposeConfiguration(TreatConfiguration)
                .BuildSessionFactory();
        }

        private static void TreatConfiguration(Configuration configuration)
        {
            // dump sql file for debug
            Action<string> updateExport = x =>
            {
                using (var file = new System.IO.FileStream(@"update.sql", System.IO.FileMode.Append, System.IO.FileAccess.Write))
                using (var sw = new System.IO.StreamWriter(file))
                {
                    sw.Write(x);
                    sw.Close();
                }
            };
            var update = new SchemaUpdate(configuration);
            update.Execute(updateExport, true);
        }
    }
}