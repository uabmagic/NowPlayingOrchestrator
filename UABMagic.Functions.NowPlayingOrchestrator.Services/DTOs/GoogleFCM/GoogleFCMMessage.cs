namespace UABMagic.Functions.NowPlayingOrchestrator.Services.DTOs.GoogleFCM;

[ExcludeFromCodeCoverage]
public class GoogleFCMMessage
{
    public string? Body { get; set; }
    public Dictionary<string, string>? Data { get; set; }
    public string? ImageUrl { get; set; }
    public string? Title { get; set; }
}
