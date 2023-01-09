namespace UABMagic.Functions.NowPlayingOrchestrator.Services.Interfaces;

public interface IUABApiClient
{
    [Get("/songs/now-playing")]
    Task<NowPlayingSong> GetNowPlayingSong();
}
