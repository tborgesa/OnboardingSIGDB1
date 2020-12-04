using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OnboardingSIGDB1.Domain.Funcionarios.Specifications
{
    public class ObterOsFuncionariosSpecification
    {
        private readonly IList<Expression<Func<Funcionario, bool>>> _filtros;

        private ObterOsFuncionariosSpecification()
        {
            _filtros = new List<Expression<Func<Funcionario, bool>>>();
        }

        public static ObterOsFuncionariosSpecification Novo() => new ObterOsFuncionariosSpecification();

        public ObterOsFuncionariosSpecification ComNome(string nome)
        {
            if (!string.IsNullOrEmpty(nome))
                _filtros.Add(_ => _.Nome.ToLower().Contains(nome.ToLower()));

            return this;
        }

        public ObterOsFuncionariosSpecification ComCpf(string cpf)
        {
            if (!string.IsNullOrEmpty(cpf))
                _filtros.Add(_ => _.Cpf == cpf.RemoverMascaraDoCpf());

            return this;
        }

        public ObterOsFuncionariosSpecification ComIntervaloDeDataDeContratacao(DateTime dataInicial, DateTime dataFinal)
        {
            if (dataInicial > DateTime.MinValue && dataFinal > DateTime.MinValue)
                _filtros.Add(_ => _.DataDeContratacao.HasValue &&
                    (_.DataDeContratacao.Value.Date >= dataInicial.Date &&
                    _.DataDeContratacao.Value.Date <= dataFinal.Date)
                    );

            return this;
        }

        public IList<Expression<Func<Funcionario, bool>>> Build() => _filtros;
    }
}
