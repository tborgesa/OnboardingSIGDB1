using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace OnboardingSIGDB1.IOC.AutoMapper.Extensions
{
    public static class AutoMapperExtension
    {
        public static T MapTo<T>(this object value)
        {
            return Mapper.Map<T>(value);
        }

        public static IEnumerable<T> EnumerableTo<T>(this object value)
        {
            return Mapper.Map<IEnumerable<T>>(value);
        }

        public static IQueryable<T> QueryableTo<T>(this object value)
        {
            return Mapper.Map<IQueryable<T>>(value);
        }
    }
}
