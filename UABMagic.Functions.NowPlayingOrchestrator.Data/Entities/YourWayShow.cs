namespace UABMagic.Functions.NowPlayingOrchestrator.Data.Entities;

[ExcludeFromCodeCoverage]
[Table("yourwayshow")]
public class YourWayShow : UABEntity
{
    [Column("username")]
    public string? Username { get; set; }
}
