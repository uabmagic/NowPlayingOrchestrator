namespace UABMagic.Functions.NowPlayingOrchestrator.Data.Utilities.Extensions;

[ExcludeFromCodeCoverage]
public static class AddDataServicesExtension
{
    public static void AddData(this IServiceCollection services)
    {
        services.AddTransient<IUABMagicUnitOfWork, UABMagicUnitOfWork>();
    }
}
