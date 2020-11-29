using Bogus;
using OnboardingSIGDB1.Domain._Base.Resources;
using System;

namespace OnboardingSIGDB1.Domain.Test.Common
{
    public static class FakerExtensions
    {
        public static int Id(this Faker faker)
        {
           return faker.Random.Int(Constantes.Numero1, Constantes.Numero100);
        }

        public static DateTime QualquerDataUltimoAno(this Faker faker)
        {
            return faker.Date.Recent(Constantes.Numero365);
        }
    }
}
