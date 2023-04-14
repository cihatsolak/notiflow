namespace Notiflow.Backoffice.Application.Mappers;

internal static class ObjectMapper
{
    internal static IMapper Mapper => lazyMapper.Value;

    private static readonly Lazy<IMapper> lazyMapper = new(() =>
    {
        MapperConfiguration mapperConfiguration = new(configuration =>
        {
            configuration.AddProfile<DeviceProfile>();
        });

        return mapperConfiguration.CreateMapper();
    });
}
