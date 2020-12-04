using System;

namespace OnboardingSIGDB1.Api.Models.Empresas
{
    public class EmpresaFiltro
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public DateTime DataDeFundacaoInicial { get; set; }
        public DateTime DataDeFundacaoFinal { get; set; }
    }
}
