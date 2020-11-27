using FluentValidation.Results;
using OnboardingSIGDB1.Domain._Base.Enumeradores;
using OnboardingSIGDB1.Domain._Base.Notification;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain._Base.Services
{
    public abstract class DomainService
    {
        protected readonly IDomainNotificationHandlerAsync NotificacaoDeDominio;

        protected DomainService(IDomainNotificationHandlerAsync notificacaoDeDominio)
        {
            NotificacaoDeDominio = notificacaoDeDominio;
        }

        public async Task NotificarValidacoesDeDominioAsync(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
                await NotificacaoDeDominio.HandleAsync(new DomainNotification(TipoDeNotificacao.ErroDeDominio.ToString(), erro.ErrorMessage));
        }

        public async Task QuandoNuloNotificarSobreDominioAsync(object dominio, string nomeDoDominio, string msg)
        {
            if (dominio == null)
                await NotificacaoDeDominio.HandleAsync(new DomainNotification(TipoDeNotificacao.ErroDeServico.ToString(),
                    string.Format(msg, nomeDoDominio)));
        }
    }
}
