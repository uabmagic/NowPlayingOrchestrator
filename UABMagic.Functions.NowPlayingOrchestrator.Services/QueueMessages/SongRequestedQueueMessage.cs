using UABMagic.Functions.NowPlayingOrchestrator.Services.DTOs.Songs;

namespace UABMagic.Functions.NowPlayingOrchestrator.Services.QueueMessages;

[ExcludeFromCodeCoverage]
public class SongRequestedQueueMessage
{
    public string? Message { get; set; }
    public NowPlayingSong? NowPlayingSong { get; set; }
    public bool PlayingNow { get; set; }
    public string? Username { get; set; }
}
