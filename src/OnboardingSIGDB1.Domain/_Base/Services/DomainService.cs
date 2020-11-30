using FluentValidation.Results;
using OnboardingSIGDB1.Domain._Base.Notification;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain._Base.Services
{
    public abstract class DomainService
    {
        protected readonly IDomainNotificationHandler NotificacaoDeDominio;

        protected DomainService(IDomainNotificationHandler notificacaoDeDominio)
        {
            NotificacaoDeDominio = notificacaoDeDominio;
        }

        public async Task NotificarValidacoesDeDominioAsync(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
                await NotificacaoDeDominio.HandleNotificacaoDeDominioAsync(erro.ErrorMessage);
        }

        public async Task QuandoNuloNotificarSobreDominioAsync(object dominio, string nomeDoDominio, string msg)
        {
            if (dominio == null)
                await NotificacaoDeDominio.HandleNotificacaoDeServicoAsync(string.Format(msg, nomeDoDominio));
        }
    }
}
