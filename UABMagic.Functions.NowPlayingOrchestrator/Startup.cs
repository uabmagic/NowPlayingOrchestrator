[assembly: FunctionsStartup(typeof(UABMagic.Functions.NowPlayingOrchestrator.Startup))]
namespace UABMagic.Functions.NowPlayingOrchestrator;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var uabApiConfig = Environment.GetEnvironmentVariable("UABApi_BaseUrl");

        builder.Services
            .AddHttpClient("UAB.Api", c =>
            {
                c.BaseAddress = new Uri(uabApiConfig);
            })
            .AddTypedClient(Refit.RestService.For<IUABApiClient>)
            .AddTransientHttpErrorPolicy(
                policy => policy.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)))
            );

        builder.Services
            .AddHttpClient("IFTTT.Api", c =>
            {
                c.BaseAddress = new Uri("https://maker.ifttt.com/trigger");
            })
            .AddTypedClient(Refit.RestService.For<IIFTTTClient>)
            .AddTransientHttpErrorPolicy(
                policy => policy.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)))
            );

        SetupGoogleFirebaseApp();

        builder.Services.AddTransientServices();
        builder.Services.AddData();
    }

    private static void SetupGoogleFirebaseApp()
    {
        var firebaseCredentials = Environment.GetEnvironmentVariable("FCMCredentials");

        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromJson(firebaseCredentials)
        });
    }
}
