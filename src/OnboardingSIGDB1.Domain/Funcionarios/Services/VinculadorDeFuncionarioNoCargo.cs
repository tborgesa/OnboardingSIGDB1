using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Cargos.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
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

        public async Task Vincular(CargoDoFuncionarioDto cargoDoFuncionarioDto)
        {
            var funcionario = await _funcionarioRepositorio.ObterPorIdAsync(cargoDoFuncionarioDto.FuncionarioId);

            var cargo = await _cargoRepositorio.ObterPorIdAsync(cargoDoFuncionarioDto.CargoId);

            var cargoDoFuncionario = new CargoDoFuncionario(funcionario, cargo, cargoDoFuncionarioDto.DataDeVinculo);

            if (!cargoDoFuncionario.Validar())
            {
                await NotificarValidacoesDeDominioAsync(cargoDoFuncionario.ValidationResult);
                return;
            }

            await _funcionarioRepositorio.AdicionarCargoParaFuncionarioAsync(cargoDoFuncionario);
        }
    }
}
