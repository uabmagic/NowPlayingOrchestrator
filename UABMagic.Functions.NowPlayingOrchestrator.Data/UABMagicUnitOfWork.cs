namespace UABMagic.Functions.NowPlayingOrchestrator.Data;

[ExcludeFromCodeCoverage]
public class UABMagicUnitOfWork : IUABMagicUnitOfWork
{
    #region Repositories

    private IPushTokenRepository? _pushTokenRepository;
    public IPushTokenRepository PushTokenRepository => _pushTokenRepository ??= new PushTokenRepository();

    #endregion
}
