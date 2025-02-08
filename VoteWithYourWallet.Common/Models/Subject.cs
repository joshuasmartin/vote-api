using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoteWithYourWallet.Common.Models;

[Table("Subjects")]
public class Subject
{
    /// <summary>
    /// Unique database ID of the subject.
    /// </summary>
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// Name of the subject, which could be something like, "Nike" or "Walmart".
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Represents the string used to lookup the subject in the database.
    /// </summary>
    public string ShortName { get; set; } = string.Empty;
    
    /// <summary>
    /// Indicates the type of subject, such as a Retailer or Brand.
    /// </summary>
    public string Type { get; set; } = string.Empty;
    
    public byte DiversityScore { get; set; }
    
    public byte UnionScore { get; set; }
    
    public byte EnvironmentalScore { get; set; }
    
    public byte LobbyingScore { get; set; }
    
    /// <summary>
    /// Timestamp at which the object was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// ID of the subject to which this subject is
    /// linked as a subsidiary or related brand.
    /// </summary>
    public int? LinkedSubjectId { get; set; }
    
    [ForeignKey("LinkedSubjectId")]
    public virtual Subject? LinkedSubject { get; set; } = null;
}
