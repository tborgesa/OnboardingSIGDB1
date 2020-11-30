using OnboardingSIGDB1.Domain._Base.Notification;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Services
{
    public class ArmazenadorDeFuncionario : DomainService, IArmazenadorDeFuncionario
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly IValidadorCpfDaFuncionarioJaExistente _validadorCpfDaFuncionarioJaExistente;

        public ArmazenadorDeFuncionario(
            IDomainNotificationHandler notificacaoDeDominio,
            IFuncionarioRepositorio funcionarioRepositorio,
            IValidadorCpfDaFuncionarioJaExistente validadorCpfDaFuncionarioJaExistente
            ) : base(notificacaoDeDominio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _validadorCpfDaFuncionarioJaExistente = validadorCpfDaFuncionarioJaExistente;
        }

        public async Task ArmazenarAsync(FuncionarioDto funcionarioDto)
        {
            funcionarioDto = funcionarioDto ?? new FuncionarioDto();

            await _validadorCpfDaFuncionarioJaExistente.ValidarAsync(funcionarioDto.Cpf, funcionarioDto.Id);

            var funcionario = funcionarioDto.Id == 0 ?
                CriarUmNovoFuncionario(funcionarioDto) :
                await EditarUmFuncionarioAsnyc(funcionarioDto);

            if (!funcionario.Validar())
                await NotificarValidacoesDeDominioAsync(funcionario.ValidationResult);

            if (!NotificacaoDeDominio.HasNotifications && funcionario.Id == 0)
                await _funcionarioRepositorio.AdicionarAsync(funcionario);
        }

        private async Task<Funcionario> EditarUmFuncionarioAsnyc(FuncionarioDto funcionarioDto)
        {
            var funcionario = await _funcionarioRepositorio.ObterPorIdAsync(funcionarioDto.Id);

            funcionario.AlterarNome(funcionarioDto.Nome);
            funcionario.AlterarCpf(funcionarioDto.Cpf);
            funcionario.AlterarDataDeContratacao(funcionarioDto.DataDeContratacao);

            return funcionario;
        }

        private Funcionario CriarUmNovoFuncionario(FuncionarioDto funcionarioDto)
        {
            return new Funcionario(funcionarioDto.Nome, funcionarioDto.Cpf, funcionarioDto.DataDeContratacao);
        }
    }
}
