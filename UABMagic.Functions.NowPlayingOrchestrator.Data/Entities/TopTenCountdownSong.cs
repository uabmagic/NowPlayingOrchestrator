namespace UABMagic.Functions.NowPlayingOrchestrator.Data.Entities;

[ExcludeFromCodeCoverage]
[Table("toptencountdownsongs")]
public class TopTenCountdownSong : UABEntity
{
    [Column("index")]
    public int Index { get; set; }

    [Column("song_id")]
    public int SongId { get; set; }

    [Column("requests")]
    public int Requests { get; set; }

    [Column("requestors")]
    public string? Requestors { get; set; }
}
