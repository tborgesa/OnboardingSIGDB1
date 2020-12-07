using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Resources;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Services
{
    public class VinculadorDeFuncionarioNaEmpresa : OnboardingSIGDB1Service
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;

        public VinculadorDeFuncionarioNaEmpresa(IDomainNotificationHandler notificacaoDeDominio,
            IFuncionarioRepositorio funcionarioRepositorio,
            IEmpresaRepositorio empresaRepositorio)
            : base(notificacaoDeDominio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _empresaRepositorio = empresaRepositorio;
        }

        public async Task Vincular(int funcionarioId, int empresaId)
        {
            var funcionario = await _funcionarioRepositorio.ObterPorIdAsync(funcionarioId);

            if (funcionario == null)
            {
                await NotificacaoDeDominio.HandleNotificacaoDeServicoAsync(FuncionarioResources.FuncionarioNaoExiste);
                return;
            }

            if (!funcionario.ValidarOVinculoComEmpresa())
            {
                await NotificarValidacoesDeDominioAsync(funcionario.ValidationResult);
                return;
            }

            var empresa = await _empresaRepositorio.ObterPorIdAsync(empresaId);

            if (empresa == null)
            {
                await NotificacaoDeDominio.HandleNotificacaoDeServicoAsync(EmpresaResources.EmpresaNaoExiste);
                return;
            }

            funcionario.VincularComEmpresa(empresa);
        }


    }
}
