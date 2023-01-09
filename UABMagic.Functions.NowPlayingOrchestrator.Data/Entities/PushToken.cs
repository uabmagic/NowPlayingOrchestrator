namespace UABMagic.Functions.NowPlayingOrchestrator.Data.Entities;

[ExcludeFromCodeCoverage]
[Table("pushtokens")]
public class PushToken : UABEntity
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("token")]
    public string? Token { get; set; }
    [Column("username")]
    public string? Username { get; set; }
    [Column("userid")]
    public int UserId { get; set; }
}
