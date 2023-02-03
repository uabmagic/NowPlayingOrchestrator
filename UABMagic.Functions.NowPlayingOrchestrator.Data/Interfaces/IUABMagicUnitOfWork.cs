namespace UABMagic.Functions.NowPlayingOrchestrator.Data.Interfaces;

public interface IUABMagicUnitOfWork
{
    IPushTokenRepository PushTokenRepository { get; }
    ITopTenCountdownSongRepository TopTenCountdownSongRepository { get; }
    IYourWayShowRepository YourWayShowRepository { get; }
}
