namespace UABMagic.Functions.NowPlayingOrchestrator.Data.Entities;

[ExcludeFromCodeCoverage]
public class UABEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
