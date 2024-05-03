using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkShortenerDatabaseLib.Entities;

[Table("links")]
[Index(nameof(NewLink), IsUnique = true)]
public class Link
{
    [Key, Column("id")]
    public int Id { get; set; }
    [MaxLength(2000), Column("old_link")]
    public string OldLink { get; set; } = null!;
    [MaxLength(50), Column("new_link")]
    public string NewLink { get; set; } = null!;
    [Column("creation_at")]
    public DateTime CreationAt { get; set; }
    [Column("expiration_date")]
    public DateTime ExpirationDate { get; set; }
    [Column("number_of_transitions")]
    public int NumberOfTransitions { get; set; }
    [Column("maximum_transitions_count")]
    public int MaximumTransitionsCount { get; set; }
}
