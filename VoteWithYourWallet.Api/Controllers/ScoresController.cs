using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoteWithYourWallet.Api.Serialization;
using VoteWithYourWallet.Common.Models;
using VoteWithYourWallet.Web;

namespace VoteWithYourWallet.Api.Controllers;

[Route("api/Subjects/{subjectId:int}/Scores")]
public class ScoresController(IConfiguration configuration, PrimaryContext context) : ControllerBase
{
    // GET: api/Subjects/1/Scores
    [HttpGet]
    public IActionResult GetScores(int subjectId = 0)
    {
        var scores = context.Scores!
            .Include(s => s.CreatedByUser)
            .Where(s => s.SubjectId == subjectId)
            .Where(s => s.IsApproved)
            .ToList();
        
        return Ok(scores);
    }
    
    // POST: api/Subjects/1/Scores
    [HttpPost]
    public async Task<IActionResult> PostSuggest([FromBody] ScoreSuggestion suggestion, int subjectId = 0)
    {
        var validTopics = new [] { "DEI", "Environment", "Unions", "Politics" };

        if (!validTopics.Contains(suggestion.Topic))
        {
            return BadRequest("Topic must be DEI, Environment, Unions, or Politics");
        }
        
        // Save the contribution to the database.
        var score = new Score()
        {
            CreatedAt = DateTime.UtcNow,
            Topic = suggestion.Topic,
            Number = (byte)suggestion.Number,
            Headline = suggestion.Headline,
            SourceUrl = suggestion.SourceUrl,
            IsApproved = false,
            CreatedByUserId = 2, // Unknown user
            SubjectId = subjectId
        };
        
        await context.AddAsync(score);
        await context.SaveChangesAsync();
            
        // Save the contribution to Azure Storage.
        var tableClient = new TableClient(configuration.GetConnectionString("ContributionsAzureStorage"),
            "scoreContributions");
        
        await tableClient.CreateIfNotExistsAsync();

        var guid = Guid.NewGuid().ToString();
        
        var tableEntity = new TableEntity(subjectId.ToString(), guid)
        {
            { "emailAddress", suggestion.EmailAddress },
            { "userName", suggestion.UserName },
            { "headline", suggestion.Headline },
            { "sourceUrl", suggestion.SourceUrl },
            { "scoreNumber", suggestion.Number },
            { "topic", suggestion.Topic },
            { "subjectId", subjectId }
        };
        
        await tableClient.AddEntityAsync(tableEntity);
        
        return Ok();
    }
}
