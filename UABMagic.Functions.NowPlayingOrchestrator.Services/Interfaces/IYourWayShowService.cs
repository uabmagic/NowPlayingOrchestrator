namespace UABMagic.Functions.NowPlayingOrchestrator.Services.Interfaces;

public interface IYourWayShowService
{
    Task HandleUABYourWayShowMessageAsync(NowPlayingSong nowPlayingSong, GetNowPlayingQueueMessage queueItem);
}
