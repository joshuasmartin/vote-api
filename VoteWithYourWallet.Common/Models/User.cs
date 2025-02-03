using System.ComponentModel.DataAnnotations;

namespace VoteWithYourWallet.Common.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
}