using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Empresas.Dto;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Resources;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Services
{
    public class EditarUmaEmpresa : OnboardingSIGDB1Service, IEditarUmaEmpresa
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;

        public EditarUmaEmpresa(IDomainNotificationHandler notificacaoDeDominio, 
            IEmpresaRepositorio empresaRepositorio)
            : base(notificacaoDeDominio)
        {
            _empresaRepositorio = empresaRepositorio;
        }

        public async Task<Empresa> EditarUmaEmpresaAsync(EmpresaDto empresaDto)
        {
            var empresa = await _empresaRepositorio.ObterPorIdAsync(empresaDto.Id);

            if (empresa == null)
                await NotificacaoDeDominio.HandleNotificacaoDeDominioAsync(
                         Resource.FormatarResource(
                             Resource.MensagemNaoExisteNoBancoDeDadosFeminino, EmpresaResources.Empresa)
                         );

            empresa?.AlterarNome(empresaDto.Nome);
            empresa?.AlterarCnpj(empresaDto.Cnpj);
            empresa?.AlterarDataDeFundacao(empresaDto.DataDeFundacao);

            return empresa;
        }
    }
}
