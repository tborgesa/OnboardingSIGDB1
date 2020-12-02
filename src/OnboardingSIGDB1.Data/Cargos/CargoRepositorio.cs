using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data._Base;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Interfaces;

namespace OnboardingSIGDB1.Data.Cargos
{
    public class CargoRepositorio : CadastroCompletoRepositorioBase<int, Cargo>, ICargoRepositorio
    {
        public CargoRepositorio(DbContext context) : base(context)
        {
        }
    }
}
