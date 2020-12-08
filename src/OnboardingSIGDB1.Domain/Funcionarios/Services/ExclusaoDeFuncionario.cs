using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Services
{
    public class ExclusaoDeFuncionario : OnboardingSIGDB1Service, IExclusaoDeFuncionario
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly IValidadorDeExclusaoDeFuncionario _validadorDeExclusaoDeFuncionario;

        public ExclusaoDeFuncionario(
            IDomainNotificationHandler notificacaoDeDominio,
            IFuncionarioRepositorio funcionarioRepositorio,
            IValidadorDeExclusaoDeFuncionario validadorDeExclusaoDeFuncionario) : base(notificacaoDeDominio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _validadorDeExclusaoDeFuncionario = validadorDeExclusaoDeFuncionario;
        }

     public async Task ExcluirAsync(int funcionarioId)
        {
            await _validadorDeExclusaoDeFuncionario.ValidarAsync(funcionarioId);

            if (NotificacaoDeDominio.HasNotifications)
                return;

            var funcionario = await _funcionarioRepositorio.ObterPorIdAsync(funcionarioId);
            _funcionarioRepositorio.Remover(funcionario);
        }
    }
}
