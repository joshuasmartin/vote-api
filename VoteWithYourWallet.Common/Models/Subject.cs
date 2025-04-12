using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoteWithYourWallet.Common.Models;

[Table("subjects")]
public class Subject
{
    /// <summary>
    /// Unique database ID of the subject.
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Name of the subject, which could be something like, "Nike" or "Walmart".
    /// </summary>
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Represents the string used to lookup the subject in the database.
    /// </summary>
    [Column("short_name")]
    public string ShortName { get; set; } = string.Empty;
    
    /// <summary>
    /// Indicates the type of subject, such as a Retailer or Brand.
    /// </summary>
    [Column("type")]
    public string Type { get; set; } = string.Empty;
    
    [Column("diversity_score")]
    public byte DiversityScore { get; set; }
    
    [Column("union_score")]
    public byte UnionScore { get; set; }
    
    [Column("environmental_score")]
    public byte EnvironmentalScore { get; set; }
    
    [Column("lobbying_score")]
    public byte LobbyingScore { get; set; }
    
    /// <summary>
    /// Timestamp at which the object was created.
    /// </summary>
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// ID of the subject to which this subject is
    /// linked as a subsidiary or related brand.
    /// </summary>
    [Column("linked_subject_id")]
    public int? LinkedSubjectId { get; set; }
    
    //[ForeignKey("linked_subject_id")]
    public virtual Subject? LinkedSubject { get; set; } = null;
}
