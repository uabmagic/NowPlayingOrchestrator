namespace UABMagic.Functions.NowPlayingOrchestrator.Services.Interfaces;

public interface IIFTTTClient
{
    [Post("/notification/with/key/{key}")]
    Task<string> PostNotification(string key, IFTTTPayload iftttPayload);
}
