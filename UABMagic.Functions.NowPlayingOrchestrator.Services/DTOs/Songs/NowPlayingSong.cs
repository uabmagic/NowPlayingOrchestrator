namespace UABMagic.Functions.NowPlayingOrchestrator.Services.DTOs.Songs;

[ExcludeFromCodeCoverage]
public class NowPlayingSong : Song
{
    public bool IsArtistBlock { get; set; }
    public bool IsUabYourWayShow { get; set; }
    public bool IsWeeklyCountdown { get; set; }
    public string? Requestor { get; set; }
    public string? Schedule { get; set; }
    public string? UabYourWayUser { get; set; }
    public IEnumerable<string>? UpNext { get; set; }
}
