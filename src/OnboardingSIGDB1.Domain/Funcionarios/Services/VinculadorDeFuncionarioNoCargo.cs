using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Cargos.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Services
{
    public class VinculadorDeFuncionarioNoCargo : OnboardingSIGDB1Service, IVinculadorDeFuncionarioNoCargo
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly ICargoRepositorio _cargoRepositorio;

        public VinculadorDeFuncionarioNoCargo(
            IDomainNotificationHandler notificacaoDeDominio,
            IFuncionarioRepositorio funcionarioRepositorio,
            ICargoRepositorio cargoRepositorio) : base(notificacaoDeDominio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _cargoRepositorio = cargoRepositorio;
        }

        public Task Vincular(CargoDoFuncionarioDto cargoDoFuncionarioDto)
        {
            throw new NotImplementedException();
        }
    }
}
