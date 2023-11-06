using Swapy.Common.Models;

namespace Swapy.BLL.Interfaces
{
    public interface INotificationService
    {
        Task Notificate(NotificationModel model);
    }
}
