namespace UABMagic.Functions.NowPlayingOrchestrator.Services.Interfaces;

public interface INowPlayingService
{
    public Task<NowPlayingSong> GetNowPlayingSongAsync();
}
