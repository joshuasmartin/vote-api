using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoteWithYourWallet.Common.Models;

[Table("scores")]
public class Score
{
    /// <summary>
    /// Unique database ID of the score.
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// ID of the subject to which this score belongs.
    /// </summary>
    [Required]
    [Column("subject_id")]
    public int SubjectId { get; set; }
    
    //[ForeignKey("subject_id")]
    public virtual Subject? Subject { get; set; } = null;
    
    /// <summary>
    /// Indicates the topic of this score, which could be something like "DEI" or "Environment"
    /// </summary>
    [Column("topic")]
    public string Topic { get; set; } = string.Empty;
    
    /// <summary>
    /// Indicates the numerical value of this score where 1 is a bad score and 3 is a good score.
    /// </summary>
    [Column("number")]
    public byte Number { get; set; }
    
    /// <summary>
    /// Timestamp at which the object was created.
    /// </summary>
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Represents a URL to an online resource to prove the score.
    /// </summary>
    [Column("source_url")]
    public string SourceUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Description of the score, could be the title or summary of an article.
    /// </summary>
    [Column("headline")]
    public string Headline { get; set; } = string.Empty;
    
    /// <summary>
    /// Indicates whether the score has been approved for display.
    /// </summary>
    [Column("is_approved")]
    public bool IsApproved { get; set; }
    
    /// <summary>
    /// ID of the user who created this score.
    /// </summary>
    [Required]
    [Column("created_by_user_id")]
    public int CreatedByUserId { get; set; }
    
    //[ForeignKey("created_by_user_id")]
    public virtual User? CreatedByUser { get; set; } = null;
}
