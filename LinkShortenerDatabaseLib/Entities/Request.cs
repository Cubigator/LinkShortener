using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkShortenerDatabaseLib.Entities;

[Table("requests")]
public class Request
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Column("ip")]
    public string Ip { get; set; } = null!;
    [Column("time")]
    public DateTime Time { get; set; }
}
