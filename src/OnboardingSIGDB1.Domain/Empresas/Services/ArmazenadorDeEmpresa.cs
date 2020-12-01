using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Empresas.Dto;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Services
{
    public class ArmazenadorDeEmpresa : OnboardingSIGDB1Service, IArmazenadorDeEmpresa
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IValidadorCnpjDaEmpresaJaExistente _validadorCnpjDaEmpresaJaExistente;

        public ArmazenadorDeEmpresa(
            IDomainNotificationHandler notificacaoDeDominio,
            IEmpresaRepositorio empresaRepositorio,
            IValidadorCnpjDaEmpresaJaExistente validadorCnpjDaEmpresaJaExistente) : base(notificacaoDeDominio)
        {
            _empresaRepositorio = empresaRepositorio;
            _validadorCnpjDaEmpresaJaExistente = validadorCnpjDaEmpresaJaExistente;
        }

        public async Task ArmazenarAsync(EmpresaDto empresaDto)
        {
            empresaDto = empresaDto ?? new EmpresaDto();

            await _validadorCnpjDaEmpresaJaExistente.ValidarAsync(empresaDto.Cnpj, empresaDto.Id);

            var empresa = empresaDto.Id > 0 ?
                await EditarUmaEmpresaAsync(empresaDto) :
                CriarUmaNovaEmpresa(empresaDto);

            if (!empresa.Validar())
                await NotificarValidacoesDeDominioAsync(empresa.ValidationResult);

            if (!NotificacaoDeDominio.HasNotifications && empresa.Id == 0)
                await _empresaRepositorio.AdicionarAsync(empresa);
        }

        private Empresa CriarUmaNovaEmpresa(EmpresaDto empresaDto)
        {
            return new Empresa(empresaDto.Nome, empresaDto.Cnpj, empresaDto.DataDeFundacao);
        }

        private async Task<Empresa> EditarUmaEmpresaAsync(EmpresaDto empresaDto)
        {
            var empresa = await _empresaRepositorio.ObterPorIdAsync(empresaDto.Id);

            empresa?.AlterarNome(empresaDto.Nome);
            empresa?.AlterarCnpj(empresaDto.Cnpj);
            empresa?.AlterarDataDeFundacao(empresaDto.DataDeFundacao);

            return empresa;
        }
    }
}
