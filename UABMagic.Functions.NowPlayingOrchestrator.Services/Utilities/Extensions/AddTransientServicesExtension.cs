namespace UABMagic.Functions.NowPlayingOrchestrator.Services.Utilities.Extensions;

[ExcludeFromCodeCoverage]
public static class AddTransientServicesExtension
{
    public static void AddTransientServices(this IServiceCollection services)
    {
        services.AddTransient<IGoogleFCMService, GoogleFCMService>();
        services.AddTransient<INowPlayingService, NowPlayingService>();
        services.AddTransient<IPushTokenService, PushTokenService>();
        services.AddTransient<IQueueMessageService, QueueMessageService>();
        services.AddTransient<ITopTenCountdownService, TopTenCountdownService>();
    }
}
