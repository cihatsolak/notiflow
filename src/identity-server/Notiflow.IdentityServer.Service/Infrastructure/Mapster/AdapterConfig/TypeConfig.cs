using Notiflow.IdentityServer.Core.Entities.Tenants;

namespace Notiflow.IdentityServer.Service.Infrastructure.Mapster.AdapterConfig;

internal class TypeConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
          .NewConfig<Tenant, TenantCacheModel>()
          .Map(dest => dest.IsSendEmailPermission, src => src.TenantPermission.IsSendEmailPermission)
          .Map(dest => dest.IsSendMessagePermission, src => src.TenantPermission.IsSendMessagePermission)
          .Map(dest => dest.IsSendNotificationPermission, src => src.TenantPermission.IsSendNotificationPermission)
          .Map(dest => dest.FirebaseServerKey, src => src.TenantApplication.FirebaseServerKey)
          .Map(dest => dest.FirebaseSenderId, src => src.TenantApplication.FirebaseSenderId)
          .Map(dest => dest.FirebaseSenderId, src => src.TenantApplication.FirebaseSenderId)
          .Map(dest => dest.HuaweiServerKey, src => src.TenantApplication.HuaweiServerKey)
          .Map(dest => dest.HuaweiSenderId, src => src.TenantApplication.HuaweiSenderId)
          .Map(dest => dest.MailFromAddress, src => src.TenantApplication.MailFromAddress)
          .Map(dest => dest.MailFromName, src => src.TenantApplication.MailFromName)
          .Map(dest => dest.MailReplyAddress, src => src.TenantApplication.MailReplyAddress);
    }
}
