using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Test._Comum;
using System;
using System.Collections.Generic;

namespace OnboardingSIGDB1.Domain.Test._Builders
{
    public class EmpresaBuilder : BuilderBase
    {
        private int _id;
        private static string _nome;
        private static string _cnpj;
        private static DateTime? _dataDeFundacao;
        private static List<Funcionario> _listaDeFuncionarios;

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

        public EmpresaBuilder ComFuncionario(Funcionario funcionario)
        {
            _listaDeFuncionarios = _listaDeFuncionarios ?? new List<Funcionario>();
            _listaDeFuncionarios.Add(funcionario);
            return this;
        }

        public Empresa Build()
        {
            var empresa = new Empresa(_nome, _cnpj, _dataDeFundacao);

            AtribuirId(_id, empresa);
            AtribuirFuncionario(empresa);
            return empresa;
        }

        private void AtribuirFuncionario(Empresa empresa)
        {
            if (_listaDeFuncionarios == null) return;
            Atribuir(_listaDeFuncionarios, "ListaDeFuncionarios", empresa);
        }
    }
}
