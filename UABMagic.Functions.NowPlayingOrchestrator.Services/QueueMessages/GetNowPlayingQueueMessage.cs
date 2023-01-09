namespace UABMagic.Functions.NowPlayingOrchestrator.Services.QueueMessages;

[ExcludeFromCodeCoverage]
public class GetNowPlayingQueueMessage
{
    public string? CurrentSchedule { get; set; }
    public bool IsUabYourWayShow { get; set; }
    public int? PreviousSongId { get; set; }
}
