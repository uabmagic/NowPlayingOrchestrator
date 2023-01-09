namespace UABMagic.Functions.NowPlayingOrchestrator.Data.Interfaces;

public interface IUABMagicUnitOfWork
{
    IPushTokenRepository PushTokenRepository { get; }
}
