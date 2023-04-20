using Notiflow.Backoffice.Application.Models.Huawei;

namespace Notiflow.Backoffice.Application.Interfaces.Services
{
    public interface IHuaweiService
    {
        Task<bool> SendNotificationsAsync(FirebaseMultipleRequest firebaseRequest, CancellationToken cancellationToken);
    }
}
