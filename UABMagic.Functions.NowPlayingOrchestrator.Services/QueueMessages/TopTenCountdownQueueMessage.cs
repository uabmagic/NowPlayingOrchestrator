using UABMagic.Functions.NowPlayingOrchestrator.Services.DTOs.Songs;

namespace UABMagic.Functions.NowPlayingOrchestrator.Services.QueueMessages;

[ExcludeFromCodeCoverage]
public class TopTenCountdownQueueMessage
{
    public int Index { get; set; }
    public NowPlayingSong? NowPlayingSong { get; set; }
    public IEnumerable<string>? Requestors { get; set; }
    public int Requests { get; set; }
}
