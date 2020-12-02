using AutoMapper;
using OnboardingSIGDB1.IOC.AutoMapper.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnboardingSIGDB1.IOC.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static IEnumerable<Type> GetAutoMapperProfiles()
        {
            var onboardingSIGDB1AutoMapperAssembly = typeof(AutoMapperConfiguration).Assembly;
            return onboardingSIGDB1AutoMapperAssembly.GetExportedTypes().
                Where(_ => _.BaseType == typeof(Profile) && !_.IsAbstract);
        }

        public static void Initialize()
        {
            Mapper.Reset();
            Mapper.Initialize((configuracoes) =>
            {
                configuracoes.AllowNullCollections = true;
                configuracoes.AddProfiles(GetAutoMapperProfiles());
                configuracoes.ForAllMaps(IgnoreUnmappedProperties);
            });
        }

        private static void IgnoreUnmappedProperties(TypeMap map, IMappingExpression expr)
        {
            foreach (string propName in map.GetUnmappedPropertyNames())
            {
                if (map.DestinationType.GetProperty(propName) != null)
                {
                    expr.ForMember(propName, opt => opt.Ignore());
                }
            }
        }
    }
}
