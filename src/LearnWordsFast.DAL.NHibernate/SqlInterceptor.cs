using System.Diagnostics;
using NHibernate;
using NHibernate.SqlCommand;

namespace LearnWordsFast.DAL.NHibernate
{
    public class SqlInterceptor : EmptyInterceptor
    {
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            Trace.WriteLine($"[pgSQL] {sql}");
            return base.OnPrepareStatement(sql);
        }
    }
}