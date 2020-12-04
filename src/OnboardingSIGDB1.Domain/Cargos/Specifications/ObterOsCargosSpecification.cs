using OnboardingSIGDB1.Domain.Cargos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OnboardingSIGDB1.Domain.Cargos.Specifications
{
    public class ObterOsCargosSpecification
    {
        private readonly IList<Expression<Func<Cargo, bool>>> _filtros;

        private ObterOsCargosSpecification()
        {
            _filtros = new List<Expression<Func<Cargo, bool>>>();
        }

        public static ObterOsCargosSpecification Novo() => new ObterOsCargosSpecification();

        public ObterOsCargosSpecification ComDescricao(string descricao)
        {
            if (!string.IsNullOrEmpty(descricao))
                _filtros.Add(_ => _.Descricao.ToLower().Contains(descricao.ToLower()));

            return this;
        }

        public IList<Expression<Func<Cargo, bool>>> Build() => _filtros;
    }
}
