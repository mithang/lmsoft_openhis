using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
    /// <summary>
    /// LINQ-style helpers for ISugarQueryable, which (unlike EF IQueryable) does
    /// not implement IEnumerable, so the usual FirstOrDefault extension does not
    /// apply. Take(1)+ToList keeps the semantics without materializing the table.
    /// </summary>
    public static class SugarQueryableExtensions
    {
        public static T FirstOrDefault<T>(this ISugarQueryable<T> queryable)
        {
            var list = queryable.Take(1).ToList();
            return list.Count > 0 ? list[0] : default;
        }

        public static T FirstOrDefault<T>(this ISugarQueryable<T> queryable, Expression<Func<T, bool>> predicate)
        {
            var list = queryable.Where(predicate).Take(1).ToList();
            return list.Count > 0 ? list[0] : default;
        }
    }
}
