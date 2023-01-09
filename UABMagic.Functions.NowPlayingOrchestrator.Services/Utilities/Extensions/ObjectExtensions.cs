namespace UABMagic.Functions.NowPlayingOrchestrator.Services.Utilities.Extensions;

public static class ObjectExtensions
{
    public static string ConvertToBase64String(this object objectToConvert)
    {
        var serializedMessage = JsonSerializer.Serialize(objectToConvert);
        var plainTextBytes = Encoding.UTF8.GetBytes(serializedMessage);

        return Convert.ToBase64String(plainTextBytes);
    }
}
