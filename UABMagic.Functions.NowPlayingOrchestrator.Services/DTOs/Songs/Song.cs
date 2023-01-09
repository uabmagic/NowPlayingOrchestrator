namespace UABMagic.Functions.NowPlayingOrchestrator.Services.DTOs.Songs;

[ExcludeFromCodeCoverage]
public class Song
{
    public int? Id { get; set; }
    public string? AttractionAndSong { get; set; }
    public Images? Images { get; set; }
    public Playback? Playback { get; set; }
    public string? ThemeParkAndLand { get; set; }
}
