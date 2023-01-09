namespace UABMagic.Functions.NowPlayingOrchestrator;

public class GetNowPlayingInformation
{
    private readonly IGoogleFCMService _googleFCMService;
    private readonly INowPlayingService _nowPlayingService;
    private readonly IPushTokenService _pushTokenService;
    private readonly IQueueMessageService _queueMessageService;

    public GetNowPlayingInformation(IGoogleFCMService googleFCMService,
        INowPlayingService nowPlayingService,
        IPushTokenService pushTokenService,
        IQueueMessageService queueMessageService)
    {
        _googleFCMService = googleFCMService ?? throw new ArgumentNullException(nameof(googleFCMService));
        _nowPlayingService = nowPlayingService ?? throw new ArgumentNullException(nameof(nowPlayingService));
        _pushTokenService = pushTokenService ?? throw new ArgumentNullException(nameof(pushTokenService));
        _queueMessageService = queueMessageService ?? throw new ArgumentNullException(nameof(queueMessageService));
    }

    [FunctionName("GetNowPlayingInformation")]
    public async Task GetNowPlaying(
        [QueueTrigger(QueueConstants.NowPlayingQueue, Connection = Constants.AzureWebJobsStorage)] GetNowPlayingQueueMessage queueItem,
        ILogger log
    )
    {
        try
        {
            var nowPlayingSong = await _nowPlayingService.GetNowPlayingSongAsync();

            await HandleUABYourWayShowMessageAsync(nowPlayingSong, queueItem);

            var shouldCreateMessages = nowPlayingSong.Id.HasValue
                && nowPlayingSong.Id.Value != 0
                && queueItem.PreviousSongId.HasValue
                && queueItem.PreviousSongId.Value != nowPlayingSong.Id.Value;

            if (shouldCreateMessages)
            {
                await CreateRequestQueueMessagesAsync(nowPlayingSong);
                await CreateTopTenCountdownMessageAsync(nowPlayingSong);
            }

            var getNowPlayingQueueMessage = new GetNowPlayingQueueMessage
            {
                CurrentSchedule = nowPlayingSong.Schedule,
                IsUabYourWayShow = nowPlayingSong.IsUabYourWayShow,
                PreviousSongId = nowPlayingSong.Id.GetValueOrDefault()
            };

            await _queueMessageService.CreateQueueMessageAsync(
                getNowPlayingQueueMessage,
                QueueConstants.NowPlayingQueue,
                nowPlayingSong.Playback.TimeLeft
            );
        }
        catch (Exception e)
        {
            log.LogError(e, $"GetNowPlayingInformation: {e.Message}");
        }
    }

    [FunctionName("SongRequested")]
    public async Task SongRequested(
        [QueueTrigger(QueueConstants.RequestsQueue, Connection = Constants.AzureWebJobsStorage)] SongRequestedQueueMessage queueItem,
        ILogger log
    )
    {
        try
        {
            var imageUrl = queueItem.NowPlayingSong == null
                ? string.Empty
                : queueItem.NowPlayingSong.Images.Url;

            var title = queueItem.PlayingNow
                ? "Your request is playing right now!"
                : "Your request is up next!";

            var userPushToken = await _pushTokenService.GetPushTokenByUsernameAsync(queueItem.Username);

            if (string.IsNullOrEmpty(userPushToken)) return;

            var tokenMessage = new TokenMessage
            {
                Body = queueItem.Message,
                Data = new Dictionary<string, string>
                {
                    { "username", queueItem.Username }
                },
                Title = title,
                Token = userPushToken
            };

            if (!string.IsNullOrEmpty(imageUrl)) tokenMessage.ImageUrl = imageUrl;

            if (queueItem.NowPlayingSong != null)
            {
                tokenMessage.Data.Add("songId", queueItem.NowPlayingSong.Id.Value.ToString());
            }

            await _googleFCMService.SendMessageToTokenAsync(tokenMessage);
        }
        catch (Exception e)
        {
            log.LogError(e, $"SongRequested: {e.Message}");
        }
    }

    [FunctionName("TopTenCountdown")]
    public async Task TopTenCountdown(
        [QueueTrigger(QueueConstants.TopTenCountdownQueue, Connection = Constants.AzureWebJobsStorage)] TopTenCountdownQueueMessage queueItem,
        ILogger log
    )
    {
        try
        {
            var formattedSong = FormatNowPlayingSongForMessage(queueItem.NowPlayingSong.AttractionAndSong,
                    queueItem.NowPlayingSong.ThemeParkAndLand);

            var pluralString = queueItem.Requests == 1 ? string.Empty : "s";

            var formattedRequestors = queueItem.Requestors.FormatAsStringWithOxfordComma();

            var body = $"{formattedSong}\n\nRequested {queueItem.Requests} time{pluralString} this week by {formattedRequestors}";

            var topicMessage = new TopicMessage
            {
                Body = body,
                Data = new Dictionary<string, string>
                {
                    {
                        "songId", queueItem.NowPlayingSong.Id.Value.ToString()
                    }
                },
                ImageUrl = queueItem.NowPlayingSong.Images.Url,
                Title = $"Top Ten Countdown Song #{queueItem.Index} with {queueItem.Requests} request{pluralString}",
                Topic = QueueConstants.TopTenCountdownQueue
            };

            await _googleFCMService.SendMessageToTopicAsync(topicMessage);
        }
        catch (Exception e)
        {
            log.LogError(e, $"TopTenCountdown: {e.Message}");
        }
    }

    private async Task CreateRequestQueueMessagesAsync(NowPlayingSong nowPlayingSong)
    {
        if (nowPlayingSong.IsWeeklyCountdown) return;

        var queueMessages = new List<SongRequestedQueueMessage>();

        if (!string.IsNullOrEmpty(nowPlayingSong.Requestor))
        {
            var message = new SongRequestedQueueMessage
            {
                Message = FormatNowPlayingSongForMessage(nowPlayingSong.AttractionAndSong, nowPlayingSong.ThemeParkAndLand),
                NowPlayingSong = nowPlayingSong,
                PlayingNow = true,
                Username = nowPlayingSong.Requestor
            };

            queueMessages.Add(message);
        }

        var requestedNextSong = nowPlayingSong.UpNext.First();

        if (requestedNextSong.ToLower().Contains("requested"))
        {
            var username = requestedNextSong[requestedNextSong.IndexOf(Constants.RequestedBy)..]
                .Replace(Constants.RequestedBy, string.Empty)
                .Replace(")", string.Empty)
                .Trim();

            queueMessages.Add(new SongRequestedQueueMessage
            {
                Message = string.Empty,
                PlayingNow = false,
                Username = username
            });
        }

        foreach (var queueMessage in queueMessages)
        {
            await _queueMessageService.CreateQueueMessageAsync(queueMessage, QueueConstants.RequestsQueue);
        }
    }

    private async Task CreateTopTenCountdownMessageAsync(NowPlayingSong nowPlayingSong)
    {
        if (!nowPlayingSong.IsWeeklyCountdown) return;

        const string withString = " with ";

        var countdownIndex = int.Parse(nowPlayingSong.Schedule[..nowPlayingSong.Schedule.IndexOf(withString)]
            .Replace("Weekly Top Ten Countdown #", string.Empty)
            .Trim());

        var requests = int.Parse(nowPlayingSong.Schedule[nowPlayingSong.Schedule.IndexOf(withString)..]
            .Replace(withString, string.Empty)
            .Replace(" request - Hosted By BoundlessRealm", string.Empty)
            .Replace(" requests - Hosted By BoundlessRealm", string.Empty)
            .Replace(" Request - Hosted By BoundlessRealm", string.Empty)
            .Replace(" Requests - Hosted By BoundlessRealm", string.Empty)
            .Trim());

        var requestors = nowPlayingSong.Requestor.Split(" / ").ToList();

        var topTenCountdownQueueMessage = new TopTenCountdownQueueMessage
        {
            Index = countdownIndex,
            NowPlayingSong = nowPlayingSong,
            Requests = requests,
            Requestors = requestors
        };

        await _queueMessageService.CreateQueueMessageAsync(topTenCountdownQueueMessage, QueueConstants.TopTenCountdownQueue);
    }

    private async Task HandleUABYourWayShowMessageAsync(NowPlayingSong nowPlayingSong, GetNowPlayingQueueMessage queueItem)
    {
        if (nowPlayingSong.IsUabYourWayShow && !queueItem.IsUabYourWayShow)
        {
            var userPushToken = await _pushTokenService.GetPushTokenByUsernameAsync(nowPlayingSong.UabYourWayUser);

            if (string.IsNullOrEmpty(userPushToken)) return;

            var tokenMessage = new TokenMessage
            {
                Body = "Your Favorites List is playing for the UAB Your Way Show!",
                Data = new Dictionary<string, string>
                {
                    { "username", nowPlayingSong.UabYourWayUser }
                },
                ImageUrl = "https://uabmagic.azureedge.net/images/profhotz.jpg",
                Title = "UAB Your Way Show",
                Token = userPushToken
            };

            await _googleFCMService.SendMessageToTokenAsync(tokenMessage);
        }
    }

    private static string FormatNowPlayingSongForMessage(string attractionAndSong, string themeParkAndLand) =>
        $"{attractionAndSong}\n{themeParkAndLand.ToUpper()}";
}