using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Resources;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Services
{
    public class ValidadorDeExclusaoDeFuncionario : OnboardingSIGDB1Service, IValidadorDeExclusaoDeFuncionario
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public ValidadorDeExclusaoDeFuncionario(
            IDomainNotificationHandler notificacaoDeDominio,
            IFuncionarioRepositorio funcionarioRepositorio) : base(notificacaoDeDominio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public async Task ValidarAsync(int funcionarioId)
        {
            var funcionario = await _funcionarioRepositorio.ObterPorIdAsync(funcionarioId);

            if (funcionario == null)
                await NotificacaoDeDominio.HandleNotificacaoDeServicoAsync(FuncionarioResources.FuncionarioNaoExiste);
        }
    }
}
