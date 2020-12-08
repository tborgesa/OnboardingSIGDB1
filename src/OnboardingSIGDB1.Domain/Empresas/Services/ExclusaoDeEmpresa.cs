using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Services
{
    public class ExclusaoDeEmpresa : OnboardingSIGDB1Service, IExclusaoDeEmpresa
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IValidadorDeExclusaoDeEmpresa _validadorDeExclusaoDeEmpresa;

        public ExclusaoDeEmpresa(
            IDomainNotificationHandler notificacaoDeDominio,
            IEmpresaRepositorio empresaRepositorio,
            IValidadorDeExclusaoDeEmpresa validadorDeExclusaoDeEmpresa) : base(notificacaoDeDominio)
        {
            _empresaRepositorio = empresaRepositorio;
            _validadorDeExclusaoDeEmpresa = validadorDeExclusaoDeEmpresa;
        }

        public async Task ExcluirAsync(int empresaId)
        {
            await _validadorDeExclusaoDeEmpresa.ValidarAsync(empresaId);

            if (NotificacaoDeDominio.HasNotifications)
                return;

            var empresa = await _empresaRepositorio.ObterPorIdAsync(empresaId);
            _empresaRepositorio.Remover(empresa);
        }
    }
}
