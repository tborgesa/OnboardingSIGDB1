using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OnboardingSIGDB1.Domain.Empresas.Specifications
{
    public class ObterAsEmpresasSpecification
    {
        private readonly IList<Expression<Func<Empresa, bool>>> _filtros;

        private ObterAsEmpresasSpecification()
        {
            _filtros = new List<Expression<Func<Empresa, bool>>>();
        }

        public static ObterAsEmpresasSpecification Novo() => new ObterAsEmpresasSpecification();

        public ObterAsEmpresasSpecification ComNome(string nome)
        {
            if (!string.IsNullOrEmpty(nome))
                _filtros.Add(_ => _.Nome.ToLower().Contains(nome.ToLower()));

            return this;
        }

        public ObterAsEmpresasSpecification ComCnpj(string cnpj)
        {
            if (!string.IsNullOrEmpty(cnpj))
                _filtros.Add(_ => _.Cnpj == cnpj.RemoverMascaraDoCnpj());

            return this;
        }

        public ObterAsEmpresasSpecification ComIntervaloDeDataDeFundacao(DateTime dataInicial, DateTime dataFinal)
        {
            if (dataInicial > DateTime.MinValue && dataFinal > DateTime.MinValue)
                _filtros.Add(_ => _.DataDeFundacao.HasValue &&
                    (_.DataDeFundacao.Value.Date >= dataInicial.Date &&
                    _.DataDeFundacao.Value.Date <= dataFinal.Date)
                    );

            return this;
        }

        public IList<Expression<Func<Empresa, bool>>> Build() => _filtros;
    }
}
