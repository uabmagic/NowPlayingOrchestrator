namespace UABMagic.Functions.NowPlayingOrchestrator.Services.Interfaces;

public interface IPushTokenService
{
    Task<string?> GetPushTokenByUsernameAsync(string username);
}
