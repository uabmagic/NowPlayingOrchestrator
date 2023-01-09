namespace UABMagic.Functions.NowPlayingOrchestrator.Services.Interfaces;

public interface IGoogleFCMService
{
    Task SendMessageToTokenAsync(TokenMessage tokenMessage);
    Task SendMessageToTopicAsync(TopicMessage topicMessage);
}
