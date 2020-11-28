using Bogus;
using OnboardingSIGDB1.Domain._Base.Resources;
using System;

namespace OnboardingSIGDB1.Domain.Test.Common
{
    public static class FakerExtensions
    {
        public static int Id(this Faker faker)
        {
           return faker.Random.Int(Constantes.QuantidadeDeCaracteres1, Constantes.QuantidadeDeCaracteres100);
        }

        public static DateTime QualquerDataUltimoAno(this Faker faker)
        {
            return faker.Date.Recent(Constantes.QuantidadeDeCaracteres365);
        }
    }
}
