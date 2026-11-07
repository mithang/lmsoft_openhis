using SqlSugar;
using System;
using System.Linq;

namespace Newtouch.CIS.Compat
{
    /// <summary>
    /// Shared identity-backfill helper (NX28 D-NX28-HERP-001, same class of fix
    /// as Herp NX26 D-NX26-KF-001 / NX27 D-NX27-HERP-002): the original EF6 base
    /// backfilled DB-generated identity values onto the entity; SqlSugar
    /// ExecuteCommand does not. Used by CIS RepositoryBase and EFDbTransaction
    /// (same assembly Newtouch.CIS.Infrastructure) so every insert path
    /// backfills the same way. No-op when the DB returns no identity (&lt;=0)
    /// or the entity has no IsIdentity property (explicit-Id tables).
    /// </summary>
    internal static class SqlSugarIdentity
    {
        internal static void Apply<T>(T entity, object identity)
        {
            if (identity == null) return;
            if (!long.TryParse(identity.ToString(), out var id) || id <= 0) return;
            var prop = typeof(T).GetProperties().FirstOrDefault(p =>
                p.GetCustomAttributes(typeof(SugarColumn), true).OfType<SugarColumn>().Any(a => a.IsIdentity));
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(entity, Convert.ChangeType(id, prop.PropertyType));
            }
        }
    }
}
