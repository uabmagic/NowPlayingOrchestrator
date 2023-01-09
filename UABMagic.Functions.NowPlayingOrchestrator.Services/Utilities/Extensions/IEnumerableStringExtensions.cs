namespace UABMagic.Functions.NowPlayingOrchestrator.Services.Utilities.Extensions;

public static class IEnumerableStringExtensions
{
    public static string FormatAsStringWithOxfordComma(this IEnumerable<string> strings)
    {
        return strings.Count() == 2
                ? string.Join(" and ", strings)
                : strings.Count() > 2
                    ? string.Join(", ", strings.Take(strings.Count() - 1).Append($"and {strings.Last()}").ToArray())
                    : string.Join(", ", strings);
    }
}
