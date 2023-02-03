namespace UABMagic.Functions.NowPlayingOrchestrator.Core.Constants;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string AzureQueueConnectionString = "AzureQueueConnectionString";
    public const string AzureWebJobsStorage = "AzureWebJobsStorage";

    public const string SupabaseKey = "SupabaseKey";
    public const string SupabaseURL = "SupabaseURL";

    public const string RequestedBy = "(Requested by: ";

    // Your Way Show
    public const string YourWayShowImageUrl = "https://uabmagic.azureedge.net/images/profhotz.jpg";
    public const string YourWayShowTokenMessageBody = "Your Favorites List is playing for the UAB Your Way Show!";
    public const string YourWayShowTokenMessageTitle = "UAB Your Way Show";
    public const string YourWayShowTokenMessageUsernameDataPropertyKey = "username";
}
