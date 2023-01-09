namespace UABMagic.Functions.NowPlayingOrchestrator.Services;

public class TopTenCountdownService : ITopTenCountdownService
{
    private readonly IQueueMessageService _queueMessageService;
    private readonly IUABMagicUnitOfWork _uabMagicUnitOfWork;

    public TopTenCountdownService(IQueueMessageService queueMessageService,
        IUABMagicUnitOfWork uabMagicUnitOfWork)
    {
        _queueMessageService = queueMessageService ?? throw new ArgumentNullException(nameof(queueMessageService));
        _uabMagicUnitOfWork = uabMagicUnitOfWork ?? throw new ArgumentNullException(nameof(uabMagicUnitOfWork));
    }

    public async Task ProcessTopTenSongAsync(NowPlayingSong nowPlayingSong)
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

        await CreateQueueMessageAsync(nowPlayingSong, countdownIndex, requests);
        await LogTopTenSongAsync(nowPlayingSong, countdownIndex, requests);
    }

    private async Task CreateQueueMessageAsync(NowPlayingSong nowPlayingSong, int countdownIndex, int requests)
    {
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

    private async Task LogTopTenSongAsync(NowPlayingSong nowPlayingSong, int countdownIndex, int requests)
    {
        var topTenCountdownSong = new TopTenCountdownSong
        {
            Index = countdownIndex,
            Requestors = nowPlayingSong.Requestor,
            Requests = requests,
            SongId = nowPlayingSong.Id.Value
        };

        await _uabMagicUnitOfWork.TopTenCountdownSongRepository.AddEntityAsync(topTenCountdownSong);
    }
}
