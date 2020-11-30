using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Test._Comum;
using System;

namespace OnboardingSIGDB1.Domain.Test._Builders
{
    public class EmpresaBuilder : BuilderBase
    {
        private int _id;
        private static string _nome;
        private static string _cnpj;
        private static DateTime? _dataDeFundacao;

        public static EmpresaBuilder Novo()
        {
            var onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();

            _nome = onboardingSIGDB1faker.FraseComQuantidadeExataDeCaracteres(Constantes.Numero150);
            _cnpj = onboardingSIGDB1faker.Cnpj();
            _dataDeFundacao = onboardingSIGDB1faker.QualquerDataDoUltimoAno();

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
