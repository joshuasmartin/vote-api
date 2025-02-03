using VoteWithYourWallet.Common.Models;

namespace VoteWithYourWallet.Web.Services;

public interface ICacheService
{
    public Task<List<Subject>> GetRetailersAsync();
    
    public Task<List<Subject>> GetBrandsAsync();
}
