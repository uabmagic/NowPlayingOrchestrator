public interface IQueueMessageService
{
    Task CreateQueueMessageAsync(object objectToSend, string queueName, int visibilityTimeoutSeconds = 0);
}
