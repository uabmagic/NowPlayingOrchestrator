namespace UABMagic.Functions.NowPlayingOrchestrator.Services.DTOs.Songs;

[ExcludeFromCodeCoverage]
public class Playback
{
    public int Duration { get; set; }
    public string? DurationDisplay { get; set; }
    public int TimeElapsed { get; set; }
    public string? TimeElapsedDisplay { get; set; }
    public int TimeLeft { get; set; }
    public string? TimeLeftDisplay { get; set; }
}
