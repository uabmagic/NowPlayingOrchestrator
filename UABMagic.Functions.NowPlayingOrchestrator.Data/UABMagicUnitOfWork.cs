namespace UABMagic.Functions.NowPlayingOrchestrator.Data;

[ExcludeFromCodeCoverage]
public class UABMagicUnitOfWork : IUABMagicUnitOfWork
{
    #region Repositories

    private IPushTokenRepository? _pushTokenRepository;
    public IPushTokenRepository PushTokenRepository => _pushTokenRepository ??= new PushTokenRepository();

    private ITopTenCountdownSongRepository? _topTenCountdownSongRepository;
    public ITopTenCountdownSongRepository TopTenCountdownSongRepository => _topTenCountdownSongRepository ??= new TopTenCountdownSongRepository();

    private IYourWayShowRepository? _yourWayShowRepository;
    public IYourWayShowRepository YourWayShowRepository => _yourWayShowRepository ??= new YourWayShowRepository();

    #endregion
}
