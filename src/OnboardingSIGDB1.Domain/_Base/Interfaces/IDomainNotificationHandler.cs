using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain._Base.Interfaces
{
    public interface IDomainNotificationHandler
    {
        bool HasNotifications { get; }
        List<DomainNotification> GetNotifications();
        void Clean();
        Task HandleNotificacaoDeServicoAsync(string mensagem);
        Task HandleNotificacaoDeDominioAsync(string mensagem);
    }
}
