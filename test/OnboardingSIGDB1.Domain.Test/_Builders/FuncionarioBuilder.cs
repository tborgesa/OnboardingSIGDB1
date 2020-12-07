using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Test._Comum;
using System;

namespace OnboardingSIGDB1.Domain.Test._Builders
{
    public class FuncionarioBuilder : BuilderBase
    {
        private int _id;
        private static string _nome;
        private static string _cpf;
        private static Empresa _empresa;
        private static DateTime? _dataDeContratacao;

        public static FuncionarioBuilder Novo()
        {
            var faker = OnboardingSIGDB1FakerBuilder.Novo().Build();

            _nome = faker.FraseComQuantidadeExataDeCaracteres(Constantes.Numero150);
            _cpf = faker.Cpf();
            _dataDeContratacao = faker.QualquerDataDoUltimoAno();
            _empresa = null;

            return new FuncionarioBuilder();
        }

        public FuncionarioBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public FuncionarioBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public FuncionarioBuilder ComCpf(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public FuncionarioBuilder ComDataDeContratacao(DateTime? dataDeContratacao)
        {
            _dataDeContratacao = dataDeContratacao;
            return this;
        }
        public FuncionarioBuilder ComEmpresa(Empresa empresa)
        {
            _empresa = empresa;
            return this;
        }

        private void AtribuirEmpresa(Funcionario funcionario)
        {
            if (_empresa == null) return;

            var typeDoFuncionario = funcionario.GetType();

            var propriedadeEmpresa = typeDoFuncionario.GetProperty("Empresa");
            var propriedadeEmpresaId = typeDoFuncionario.GetProperty("EmpresaId");

            propriedadeEmpresa.SetValue(funcionario, _empresa);
            propriedadeEmpresaId.SetValue(funcionario, _empresa?.Id);

        }

        public Funcionario Build()
        {
            var funcionario = new Funcionario(_nome, _cpf, _dataDeContratacao);

            AtribuirId(_id, funcionario);
            AtribuirEmpresa(funcionario);

            return funcionario;
        }

    }
}
