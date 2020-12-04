using System;

namespace OnboardingSIGDB1.Api.Models.Funcionarios
{
    public class FuncionarioFiltro
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataDeContratacaoInicial { get; set; }
        public DateTime DataDeContratacaoFinal { get; set; }
    }
}
