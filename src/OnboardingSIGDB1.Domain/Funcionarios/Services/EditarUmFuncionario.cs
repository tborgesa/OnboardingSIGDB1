using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Resources;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Services
{
    public class EditarUmFuncionario : OnboardingSIGDB1Service, IEditarUmFuncionario
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public EditarUmFuncionario(IDomainNotificationHandler notificacaoDeDominio,
            IFuncionarioRepositorio funcionarioRepositorio) : base(notificacaoDeDominio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public async Task<Funcionario> EditarAsync(FuncionarioDto funcionarioDto)
        {
            var funcionario = await _funcionarioRepositorio.ObterPorIdAsync(funcionarioDto.Id);

            if (funcionario == null)
                await NotificacaoDeDominio.HandleNotificacaoDeServicoAsync(FuncionarioResources.FuncionarioNaoExiste);

            funcionario?.AlterarNome(funcionarioDto.Nome);
            funcionario?.AlterarCpf(funcionarioDto.Cpf);
            funcionario?.AlterarDataDeContratacao(funcionarioDto.DataDeContratacao);

            return funcionario;
        }

    }
}
