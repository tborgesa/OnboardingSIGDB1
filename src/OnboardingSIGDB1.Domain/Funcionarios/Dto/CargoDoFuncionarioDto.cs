using System;

namespace OnboardingSIGDB1.Domain.Funcionarios.Dto
{
    public class CargoDoFuncionarioDto
    {
        public int FuncionarioId { get; set; }
        public int CargoId { get; set; }
        public DateTime DataDeVinculo { get; set; }
    }
}
