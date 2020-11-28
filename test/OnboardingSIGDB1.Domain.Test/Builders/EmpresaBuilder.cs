using Bogus.Extensions.Brazil;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Test.Common;
using System;

namespace OnboardingSIGDB1.Domain.Test.Builders
{
    public class EmpresaBuilder : BuilderBase
    {
        private int _id;
        private static string _nome;
        private static string _cnpj;
        private static DateTime? _dataDeFundacao;

        public static EmpresaBuilder Novo()
        {
            var faker = FakerBuilder.Novo().Build();

            _nome = faker.Lorem.Random.AlphaNumeric(Constantes.QuantidadeDeCaracteres150);
            _cnpj = faker.Company.Cnpj();
            _dataDeFundacao = faker.QualquerDataUltimoAno();

            return new EmpresaBuilder();
        }

        public EmpresaBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public EmpresaBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public EmpresaBuilder ComCnpj(string cnpj)
        {
            _cnpj = cnpj;
            return this;
        }

        public EmpresaBuilder ComDataDeFundacao(DateTime? dataDeFundacao)
        {
            _dataDeFundacao = dataDeFundacao;
            return this;
        }

        public Empresa Build()
        {
            var empresa = new Empresa(_nome, _cnpj, _dataDeFundacao);

            AtribuirId(_id, empresa);

            return empresa;
        }
    }
}
