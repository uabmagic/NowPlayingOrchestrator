namespace UABMagic.Functions.NowPlayingOrchestrator.Data.Entities;

[ExcludeFromCodeCoverage]
public class UABEntity : BaseModel
{
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
