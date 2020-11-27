using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain._Base.Notification
{
    public interface IDomainNotificationHandlerAsync
    {
        bool HasNotifications { get; }
        List<DomainNotification> GetNotifications();
        void Clean();
        Task HandleAsync(DomainNotification notification);
        Task HandleNotificacaoDeServicoAsync(string mensagem);
        Task HandleNotificacaoDeDominioAsync(string mensagem);
    }
}
