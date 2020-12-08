using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Resources;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Services
{
    public class ValidadorDeExclusaoDeEmpresa : OnboardingSIGDB1Service, IValidadorDeExclusaoDeEmpresa
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;

        public ValidadorDeExclusaoDeEmpresa(
            IDomainNotificationHandler notificacaoDeDominio,
            IEmpresaRepositorio empresaRepositorio) : base(notificacaoDeDominio)
        {
            _empresaRepositorio = empresaRepositorio;
        }

        public async Task ValidarAsync(int empresaId)
        {
            var empresa = await _empresaRepositorio.ObterPorIdAsync(empresaId);

            if (empresa == null)
            {
                await NotificacaoDeDominio.HandleNotificacaoDeServicoAsync(EmpresaResources.EmpresaNaoExiste);
                return;
            }

            if (empresa.ListaDeFuncionarios.Any())
                await NotificacaoDeDominio.HandleNotificacaoDeServicoAsync(EmpresaResources.ExisteFuncionarioVinculadoNaEmpresa);
        }
    }
}
