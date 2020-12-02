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
        private readonly IEditarUmaEmpresa _editarUmaEmpresaRepositorio;
        private readonly IValidadorCnpjDaEmpresaJaExistente _validadorCnpjDaEmpresaJaExistente;

        public ArmazenadorDeEmpresa(
            IDomainNotificationHandler notificacaoDeDominio,
            IEmpresaRepositorio empresaRepositorio,
            IValidadorCnpjDaEmpresaJaExistente validadorCnpjDaEmpresaJaExistente,
            IEditarUmaEmpresa editarUmaEmpresaRepositorio) : base(notificacaoDeDominio)
        {
            _empresaRepositorio = empresaRepositorio;
            _validadorCnpjDaEmpresaJaExistente = validadorCnpjDaEmpresaJaExistente;
            _editarUmaEmpresaRepositorio = editarUmaEmpresaRepositorio;
        }

        public async Task ArmazenarAsync(EmpresaDto empresaDto)
        {
            empresaDto = empresaDto ?? new EmpresaDto();

            var empresa = empresaDto.Id > 0 ?
                await _editarUmaEmpresaRepositorio.EditarAsync(empresaDto) :
                CriarUmaNovaEmpresa(empresaDto);

            if (NotificacaoDeDominio.HasNotifications)
                return;

            if (!empresa.Validar())
                await NotificarValidacoesDeDominioAsync(empresa.ValidationResult);

            await _validadorCnpjDaEmpresaJaExistente.ValidarAsync(empresa.Cnpj, empresa.Id);

            if (!NotificacaoDeDominio.HasNotifications && empresa.Id == 0)
                await _empresaRepositorio.AdicionarAsync(empresa);
        }

        private Empresa CriarUmaNovaEmpresa(EmpresaDto empresaDto)
        {
            return new Empresa(empresaDto.Nome, empresaDto.Cnpj, empresaDto.DataDeFundacao);
        }
    }
}
