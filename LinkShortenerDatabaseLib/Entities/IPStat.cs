using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkShortenerDatabaseLib.Entities;

[Table("ip_stats")]
[Index(nameof(Ip), IsUnique = true)]
public class IPStat
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Column("ip")]
    public string Ip { get; set; } = null!;
    [Column("last_request")]
    public DateTime LastRequest { get; set; }
    [Column("requests_count")]
    public int RequestsCount { get; set; }
}
