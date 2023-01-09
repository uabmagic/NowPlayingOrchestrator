using FirebaseAdmin.Messaging;

namespace UABMagic.Functions.NowPlayingOrchestrator.Services;

public class GoogleFCMService : IGoogleFCMService
{
    public async Task SendMessageToTokenAsync(TokenMessage tokenMessage)
    {
        var message = BuildMessage(tokenMessage);

        message.Token = tokenMessage.Token;

        await FirebaseMessaging.DefaultInstance.SendAsync(message);
    }

    public async Task SendMessageToTopicAsync(TopicMessage topicMessage)
    {
        var message = BuildMessage(topicMessage);

        message.Topic = topicMessage.Topic;

        await FirebaseMessaging.DefaultInstance.SendAsync(message);
    }

    private static Message BuildMessage(GoogleFCMMessage googleFCMData)
    {
        //var jsonSerializerOptions = new JsonSerializerOptions
        //{
        //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //};

        //var serializedMessage = JsonSerializer.Serialize(googleFCMData, jsonSerializerOptions);

        return new Message
        {
            Notification = new Notification
            {
                Body = googleFCMData.Body,
                ImageUrl = googleFCMData.ImageUrl,
                Title = googleFCMData.Title
            }
        };

        //if (!string.IsNullOrWhiteSpace(googleFCMData.Token))
        //{
        //    message.Token = googleFCMData.Token;
        //}

        //if (!string.IsNullOrWhiteSpace(googleFCMData.Topic))
        //{
        //    message.Token = googleFCMData.Topic;
        //}

        //return message;
    }
}
