using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoteWithYourWallet.Common.Models;

public class Score
{
    /// <summary>
    /// Unique database ID of the score.
    /// </summary>
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// ID of the subject to which this score belongs.
    /// </summary>
    [Required]
    public int SubjectId { get; set; }
    
    [ForeignKey("SubjectId")]
    public virtual Subject? Subject { get; set; } = null;
    
    /// <summary>
    /// Indicates the topic of this score, which could be something like "DEI" or "Environment"
    /// </summary>
    public string Topic { get; set; } = string.Empty;
    
    /// <summary>
    /// Indicates the numerical value of this score where 1 is a bad score and 3 is a good score.
    /// </summary>
    public byte Number { get; set; }
    
    /// <summary>
    /// Timestamp at which the object was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Represents a URL to an online resource to prove the score.
    /// </summary>
    public string SourceUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Description of the score, could be the title or summary of an article.
    /// </summary>
    public string Headline { get; set; } = string.Empty;
    
    /// <summary>
    /// Indicates whether the score has been approved for display.
    /// </summary>
    public bool IsApproved { get; set; }
    
    /// <summary>
    /// ID of the user who created this score.
    /// </summary>
    [Required]
    public int CreatedByUserId { get; set; }
    
    [ForeignKey("CreatedByUserId")]
    public virtual User? CreatedByUser { get; set; } = null;
}
