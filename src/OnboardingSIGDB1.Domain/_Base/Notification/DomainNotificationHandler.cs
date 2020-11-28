using OnboardingSIGDB1.Domain._Base.Enumeradores;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain._Base.Notification
{
    public class DomainNotificationHandler : IDomainNotificationHandler
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public void Clean()
        {
            _notifications = new List<DomainNotification>();
        }

        public List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public Task HandleAsync(DomainNotification notification)
        {
            _notifications.Add(notification);
            return Task.CompletedTask;
        }

        public Task HandleNotificacaoDeDominioAsync(string mensagem)
        {
            _notifications.Add(new DomainNotification(TipoDeNotificacao.ErroDeDominio.ToString(), mensagem));
            return Task.CompletedTask;
        }

        public Task HandleNotificacaoDeServicoAsync(string mensagem)
        {
            _notifications.Add(new DomainNotification(TipoDeNotificacao.ErroDeServico.ToString(), mensagem));
            return Task.CompletedTask;
        }

        public bool HasNotifications => GetNotifications().Any();
    }
}
