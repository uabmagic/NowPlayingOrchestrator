namespace UABMagic.Functions.NowPlayingOrchestrator.Services;

[ExcludeFromCodeCoverage]
public class NowPlayingService : INowPlayingService
{
    private readonly IUABApiClient _uabApiClient;

    public NowPlayingService(IUABApiClient uabApiClient)
    {
        _uabApiClient = uabApiClient ?? throw new ArgumentNullException(nameof(uabApiClient));
    }

    public async Task<NowPlayingSong> GetNowPlayingSongAsync()
    {
        return await _uabApiClient.GetNowPlayingSong();
    }
}
