namespace VoteWithYourWallet.Api.Serialization;

public class ScoreSuggestion
{
    public int Number { get; set; }
    
    public string Headline { get; set; } = string.Empty;
    
    public string SourceUrl { get; set; } = string.Empty;
    
    public string Topic { get; set; } = string.Empty;
    
    public string UserName { get; set; } = string.Empty;
    
    public string EmailAddress { get; set; } = string.Empty;
}
