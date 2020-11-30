using Bogus;
using Bogus.Extensions.Brazil;
using OnboardingSIGDB1.Domain._Base.Resources;
using System;
using System.Linq;

namespace OnboardingSIGDB1.Domain.Test._Comum
{
    public class OnboardingSIGDB1Faker
    {
        public readonly Faker Faker;

        public OnboardingSIGDB1Faker(string linguagem)
        {
            Faker = new Faker(linguagem);
        }
        public int Id() => Faker.Random.Int(Constantes.Numero1, Constantes.Numero100);
        
        public DateTime QualquerDataDoUltimoAno() => Faker.Date.Past(Constantes.Numero1);
        
        public string Cnpj() => Faker.Company.Cnpj();

        public string Cpf() => Faker.Person.Cpf();
        
        public string FraseComQuantidadeExataDeCaracteres(int quantidade)
        {
            return Faker.Lorem.Sentence(quantidade).Substring(0, quantidade);
        }

        public string NumeroComQuantidadeExataDeCaracteresComoString(int quantidade) => string.Join("", Faker.Random.Digits(quantidade));
    }
}
