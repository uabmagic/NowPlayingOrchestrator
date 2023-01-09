namespace UABMagic.Functions.NowPlayingOrchestrator.Services.Interfaces;

public interface ITopTenCountdownService
{
    Task ProcessTopTenSongAsync(NowPlayingSong nowPlayingSong);
}
