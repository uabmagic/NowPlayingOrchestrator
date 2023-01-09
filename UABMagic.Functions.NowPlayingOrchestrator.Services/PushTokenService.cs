namespace UABMagic.Functions.NowPlayingOrchestrator.Services;

public class PushTokenService : IPushTokenService
{
    private readonly IUABMagicUnitOfWork _uabMagicUnitOfWork;

    public PushTokenService(IUABMagicUnitOfWork uabMagicUnitOfWork)
    {
        _uabMagicUnitOfWork = uabMagicUnitOfWork ?? throw new ArgumentNullException(nameof(uabMagicUnitOfWork));
    }

    public async Task<string?> GetPushTokenByUsernameAsync(string username)
    {
        var pushTokens = await _uabMagicUnitOfWork.PushTokenRepository.GetAllAsync();

        return pushTokens.SingleOrDefault(pushToken => pushToken.Username == username)?.Token;
    }
}
