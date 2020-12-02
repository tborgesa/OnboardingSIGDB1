using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Resources;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Services
{
    public class ValidadorCnpjDaEmpresaJaExistente : OnboardingSIGDB1Service, IValidadorCnpjDaEmpresaJaExistente
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;

        public ValidadorCnpjDaEmpresaJaExistente(
            IDomainNotificationHandler notificacaoDeDominio,
            IEmpresaRepositorio empresaRepositorio) : base(notificacaoDeDominio)
        {
            _empresaRepositorio = empresaRepositorio;
        }

        public async Task<bool> ValidarAsync(string cnpj, int idDaEntidadeAtual)
        {
            var empresa = await _empresaRepositorio.ObterPorCnpjAsync(cnpj);

            if (empresa != null && empresa.Id != idDaEntidadeAtual)
                await NotificacaoDeDominio.HandleNotificacaoDeServicoAsync(
                     Resource.FormatarResourceToLowerValor2(
                         Resource.MensagemJaExisteCadastradoFeminino,
                         EmpresaResources.Empresa, EmpresaResources.Cnpj)
                     );

            return !NotificacaoDeDominio.HasNotifications;
        }
    }
}
