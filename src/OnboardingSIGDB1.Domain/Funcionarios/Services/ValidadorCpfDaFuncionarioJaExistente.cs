using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Resources;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Services
{
    public class ValidadorCpfDaFuncionarioJaExistente : OnboardingSIGDB1Service, IValidadorCpfDaFuncionarioJaExistente
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public ValidadorCpfDaFuncionarioJaExistente(
            IDomainNotificationHandler notificacaoDeDominio,
            IFuncionarioRepositorio funcionarioRepositorio) : base(notificacaoDeDominio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public async Task<bool> ValidarAsync(string cpf, int? id = null)
        {
            var funcionario = await _funcionarioRepositorio.ObterPorCpfAsync(cpf);

            if (funcionario != null && funcionario.Id != id)
                await NotificacaoDeDominio.HandleNotificacaoDeServicoAsync(
                     Resource.FormatarResourceToLowerValor2(
                        Resource.MensagemJaExisteCadastradoMasculino,
                        FuncionarioResources.Funcionario, FuncionarioResources.Cpf));

            return !NotificacaoDeDominio.HasNotifications;
        }
    }
}
