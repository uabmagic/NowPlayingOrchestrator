namespace UABMagic.Functions.NowPlayingOrchestrator.Services;

[ExcludeFromCodeCoverage]
public class QueueMessageService : IQueueMessageService
{
    private readonly string? _azureWebJobsStorageConnectionString;

    public QueueMessageService()
    {
        _azureWebJobsStorageConnectionString = Environment.GetEnvironmentVariable(Constants.AzureWebJobsStorage);
    }

    public async Task CreateQueueMessageAsync(object objectToSend, string queueName, int visibilityTimeoutSeconds = 0)
    {
        var base64ConvertedObject = objectToSend.ConvertToBase64String();

        var queueClient = new QueueClient(_azureWebJobsStorageConnectionString, queueName);
        await queueClient.SendMessageAsync(base64ConvertedObject, visibilityTimeout: TimeSpan.FromSeconds(visibilityTimeoutSeconds));
    }
}
