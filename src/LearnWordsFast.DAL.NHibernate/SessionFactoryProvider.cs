using System;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.OptionsModel;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Environment = System.Environment;

namespace LearnWordsFast.DAL.NHibernate
{
    public class SessionFactoryProvider : ISessionFactoryProvider
    {
        private readonly NHibernateOptions options;

        public SessionFactoryProvider(IOptions<NHibernateOptions> options)
        {
            this.options = options.Value;
        }

        public ISessionFactory GetSessionFactory()
        {
            return Fluently
                .Configure()
                    .Database(
                        PostgreSQLConfiguration.PostgreSQL82
                        .ConnectionString(c =>
                            c.Host(options.Host)
                            .Port(options.Port)
                            .Database(options.Database)
                            .Username(options.User)
                            .Password(options.Password)))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ModelMappings.WordMapping>())
                    .ExposeConfiguration(TreatConfiguration)
                .BuildSessionFactory();
        }

        private static void TreatConfiguration(Configuration configuration)
        {
            configuration.SetInterceptor(new SqlInterceptor());

            // dump sql file for debug
            Action<string> updateExport = x =>
            {
                var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var folder = Path.Combine(appdata, "LearnWordsFast");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var filePath = Path.Combine(folder, $"update_{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}.sql");
                using (var file = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    using (var sw = new StreamWriter(file))
                    {
                        sw.Write(x);
                        sw.Close();
                    }
                }
            };

            var update = new SchemaUpdate(configuration);
            update.Execute(updateExport, true);
        }

        public ISessionFactory Get()
        {
            return GetSessionFactory();
        }
    }
}