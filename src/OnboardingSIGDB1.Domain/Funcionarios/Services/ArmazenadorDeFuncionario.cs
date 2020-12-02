using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Services
{
    public class ArmazenadorDeFuncionario : OnboardingSIGDB1Service, IArmazenadorDeFuncionario
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly IValidadorCpfDaFuncionarioJaExistente _validadorCpfDaFuncionarioJaExistente;
        private readonly IEditarUmFuncionario _editarUmFuncionario;

        public ArmazenadorDeFuncionario(
            IDomainNotificationHandler notificacaoDeDominio,
            IFuncionarioRepositorio funcionarioRepositorio,
            IValidadorCpfDaFuncionarioJaExistente validadorCpfDaFuncionarioJaExistente,
            IEditarUmFuncionario editarUmFuncionario) : base(notificacaoDeDominio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _validadorCpfDaFuncionarioJaExistente = validadorCpfDaFuncionarioJaExistente;
            _editarUmFuncionario = editarUmFuncionario;
        }

        public async Task ArmazenarAsync(FuncionarioDto funcionarioDto)
        {
            funcionarioDto = funcionarioDto ?? new FuncionarioDto();

            var funcionario = funcionarioDto.Id == 0 ?
                CriarUmNovoFuncionario(funcionarioDto) :
                await _editarUmFuncionario.EditarAsync(funcionarioDto);

            if (NotificacaoDeDominio.HasNotifications)
                return;

            if (!funcionario.Validar())
                await NotificarValidacoesDeDominioAsync(funcionario.ValidationResult);

            await _validadorCpfDaFuncionarioJaExistente.ValidarAsync(funcionario.Cpf, funcionario.Id);

            if (!NotificacaoDeDominio.HasNotifications && funcionario.Id == 0)
                await _funcionarioRepositorio.AdicionarAsync(funcionario);
        }

        private Funcionario CriarUmNovoFuncionario(FuncionarioDto funcionarioDto)
        {
            return new Funcionario(funcionarioDto.Nome, funcionarioDto.Cpf, funcionarioDto.DataDeContratacao);
        }
    }
}
