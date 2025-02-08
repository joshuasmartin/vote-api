using Microsoft.EntityFrameworkCore;
using VoteWithYourWallet.Common.Models;

namespace VoteWithYourWallet.Web.Services;

public class CacheService(IDbContextFactory<PrimaryContext> dbFactory) : ICacheService
{
    private DateTime _retailersCacheExpiration = DateTime.UtcNow.AddHours(1);
    
    private DateTime _brandsCacheExpiration = DateTime.UtcNow.AddHours(1);

    private List<Subject>? _retailers;
    
    private List<Subject>? _brands;
    
    public async Task<List<Subject>> GetRetailersAsync()
    {
        if (_retailersCacheExpiration > DateTime.UtcNow && _retailers != null)
        {
            return _retailers;
        }
        
        await using var context = await dbFactory.CreateDbContextAsync();
        
        _retailers =  await context.Subjects!
            .Where(s => s.Type == "Retailer")
            .OrderBy(s => s.Name)
            .ToListAsync();
        
        _retailersCacheExpiration = DateTime.UtcNow.AddHours(1);

        return _retailers;
    }

    public async Task<List<Subject>> GetBrandsAsync()
    {
        if (_brandsCacheExpiration > DateTime.UtcNow && _brands != null)
        {
            return _brands;
        }
        
        await using var context = await dbFactory.CreateDbContextAsync();
        
        _brands = await context.Subjects!
            .Where(s => s.Type == "Brand")
            .OrderBy(s => s.Name)
            .ToListAsync();
        
        _brandsCacheExpiration = DateTime.UtcNow.AddHours(1);

        return _brands;
    }

    public void BustCache()
    {
        _retailersCacheExpiration = DateTime.UtcNow.AddHours(-2);
        _brandsCacheExpiration = DateTime.UtcNow.AddHours(-2);
    }
}
