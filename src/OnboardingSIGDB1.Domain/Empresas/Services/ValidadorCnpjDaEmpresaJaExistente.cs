using OnboardingSIGDB1.Domain._Base.Notification;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Services
{
    public class ValidadorCnpjDaEmpresaJaExistente : DomainService, IValidadorCnpjDaEmpresaJaExistente
    {
        IEmpresaRepositorio _empresaRepositorio;

        public ValidadorCnpjDaEmpresaJaExistente(
            IDomainNotificationHandlerAsync notificacaoDeDominio,
            IEmpresaRepositorio empresaRepositorio) : base(notificacaoDeDominio)
        {
            _empresaRepositorio = empresaRepositorio;
        }

        public async Task<bool> ValidarAsync(string cnpj, int? id = null)
        {
            var empresa = await _empresaRepositorio.ObterPorCnpjAsync(cnpj);

            if (empresa != null && empresa.Id != id)
                await NotificacaoDeDominio.HandleNotificacaoDeServicoAsync(
                     Resource.FormatarResourceToLowerValor2(
                         Resource.MensagemJaExisteCadastrada,
                         EmpresaResources.Empresa, EmpresaResources.Cnpj)
                     );

            return !NotificacaoDeDominio.HasNotifications;
        }
    }
}
