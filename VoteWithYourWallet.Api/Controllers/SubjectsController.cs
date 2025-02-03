using System.Text.Json;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using VoteWithYourWallet.Api.Serialization;
using VoteWithYourWallet.Common.Models;
using VoteWithYourWallet.Web;
using VoteWithYourWallet.Web.Components.Pages;

namespace VoteWithYourWallet.Api.Controllers;

[Route("api/Subjects")]
public class SubjectsController(IConfiguration configuration, PrimaryContext context, IMemoryCache memoryCache) : ControllerBase
{
    // GET: api/Subjects/Retailers
    [HttpGet("Retailers")]
    public IActionResult GetRetailers([FromQuery] int skip = 0, [FromQuery] int pageSize = 1000)
    {
        // Attempt to get the object from the cache.
        const string cacheKey = "Subjects/Retailers";
        if (memoryCache.Get(cacheKey) is string data)
        {
            var obj = JsonSerializer.Deserialize<List<Subject>>(data);

            return Ok(obj);
        }
        
        var subjects = context.Subjects!
            .Where(s => s.Type == "Retailer")
            .OrderBy(s => s.Name)
            .ToList();
        
        // Cache the results.
        memoryCache.Set(cacheKey, JsonSerializer.Serialize(subjects), DateTimeOffset.UtcNow.AddHours(1));
        
        return Ok(subjects);
    }
    
    // GET: api/Subjects/Brands
    [HttpGet("Brands")]
    public IActionResult GetBrands([FromQuery] int skip = 0, [FromQuery] int pageSize = 1000)
    {
        // Attempt to get the object from the cache.
        const string cacheKey = "Subjects/Brands";
        if (memoryCache.Get(cacheKey) is string data)
        {
            var obj = JsonSerializer.Deserialize<List<Subject>>(data);

            return Ok(obj);
        }
        
        var subjects = context.Subjects!
            .Where(s => s.Type == "Brand")
            .OrderBy(s => s.Name)
            .ToList();
        
        // Cache the results.
        memoryCache.Set(cacheKey, JsonSerializer.Serialize(subjects), DateTimeOffset.UtcNow.AddHours(1));
        
        return Ok(subjects);
    }
    
    // GET: api/Subjects/1
    [HttpGet("{id:int}")]
    public IActionResult GetSubject(int id = 0)
    {
        // Attempt to get the object from the cache.
        var cacheKey = $"Subjects/{id}";
        if (memoryCache.Get(cacheKey) is string data)
        {
            var obj = JsonSerializer.Deserialize<Subject>(data);

            return Ok(obj);
        }
        
        var subject = context.Subjects!
            .FirstOrDefault(s => s.Id == id);
        
        // Cache the results.
        memoryCache.Set(cacheKey, JsonSerializer.Serialize(subject), DateTimeOffset.UtcNow.AddHours(1));
        
        return Ok(subject);
    }
    
    // POST: api/Subjects/Suggest
    [HttpPost("Suggest")]
    public async Task<IActionResult> PostSuggest([FromBody] SubjectSuggestion suggestion)
    {
        var validSubjects = new [] { "Retailer", "Brand" };

        if (!validSubjects.Contains(suggestion.Type))
        {
            return BadRequest("Type must be Retailer or Brand");
        }
        
        var tableClient = new TableClient(configuration.GetConnectionString("ContributionsAzureStorage"),
            "subjectContributions");
        
        await tableClient.CreateIfNotExistsAsync();

        var guid = Guid.NewGuid().ToString();
        
        var tableEntity = new TableEntity(suggestion.Type, guid)
        {
            { "emailAddress", suggestion.EmailAddress },
            { "userName", suggestion.UserName },
            { "subjectName", suggestion.Name },
            { "subjectType", suggestion.Type }
        };
        
        await tableClient.AddEntityAsync(tableEntity);
        
        return Ok();
    }
}
