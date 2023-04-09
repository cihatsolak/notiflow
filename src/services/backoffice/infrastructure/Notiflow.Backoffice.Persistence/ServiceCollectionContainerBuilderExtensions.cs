namespace Notiflow.Backoffice.Persistence
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        public static void Add(this IServiceCollection services)
        {
            services.AddScoped<IFirebaseRepository, FirebaseRepository>();
        }
    }
}
