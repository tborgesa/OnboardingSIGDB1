using System;

namespace OnboardingSIGDB1.Domain.Funcionarios.Dto
{
    public class FuncionarioComEmpresaECargoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataDeContratacao { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
    }
}
