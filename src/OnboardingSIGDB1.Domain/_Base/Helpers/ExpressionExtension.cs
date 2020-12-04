using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OnboardingSIGDB1.Domain._Base.Helpers
{
    public static class ExpressionExtension
    {
        public static IQueryable<T> FiltrarUmaListaDeWhere<T>(this IQueryable<T> query, IEnumerable<Expression<Func<T, bool>>> predicate)
        {
            foreach (var item in predicate)
            {
                query = query.Where(item);
            }

            return query;
        }
    }
}
