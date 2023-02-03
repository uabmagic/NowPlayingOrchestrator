namespace UABMagic.Functions.NowPlayingOrchestrator.Services;

public class YourWayShowService : IYourWayShowService
{
    private readonly IGoogleFCMService _googleFCMService;
    private readonly IPushTokenService _pushTokenService;
    private readonly IUABMagicUnitOfWork _uabMagicUnitOfWork;

    public YourWayShowService(IGoogleFCMService googleFCMService,
        IPushTokenService pushTokenService,
        IUABMagicUnitOfWork uabMagicUnitOfWork)
    {
        _googleFCMService = googleFCMService ?? throw new ArgumentNullException(nameof(googleFCMService));
        _pushTokenService = pushTokenService ?? throw new ArgumentNullException(nameof(pushTokenService));
        _uabMagicUnitOfWork = uabMagicUnitOfWork ?? throw new ArgumentNullException(nameof(uabMagicUnitOfWork));
    }

    public async Task HandleUABYourWayShowMessageAsync(NowPlayingSong nowPlayingSong, GetNowPlayingQueueMessage queueItem)
    {
        if (nowPlayingSong.IsUabYourWayShow && !queueItem.IsUabYourWayShow)
        {
            var username = nowPlayingSong.UabYourWayUser;

            if (string.IsNullOrEmpty(username)) return;

            await CreateAndSendPushMessage(username);
            await LogYourWayShowEntry(username);
        }
    }

    private async Task CreateAndSendPushMessage(string username)
    {
        var userPushToken = await _pushTokenService.GetPushTokenByUsernameAsync(username);

        if (string.IsNullOrEmpty(userPushToken)) return;

        var tokenMessage = new TokenMessage
        {
            Body = Constants.YourWayShowTokenMessageBody,
            Data = new Dictionary<string, string>
                {
                    {
                        Constants.YourWayShowTokenMessageUsernameDataPropertyKey, username
                    }
                },
            ImageUrl = Constants.YourWayShowImageUrl,
            Title = Constants.YourWayShowTokenMessageTitle,
            Token = userPushToken
        };

        await _googleFCMService.SendMessageToTokenAsync(tokenMessage);
    }

    private async Task LogYourWayShowEntry(string username)
    {
        var yourWayShowEntry = new YourWayShow { Username = username };

        await _uabMagicUnitOfWork.YourWayShowRepository.AddEntityAsync(yourWayShowEntry);
    }
}
