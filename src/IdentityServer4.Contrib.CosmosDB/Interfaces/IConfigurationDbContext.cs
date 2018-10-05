using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace IdentityServer4.Contrib.CosmosDB.Interfaces
{
    public interface IConfigurationDbContext
    {
        IQueryable<Client> Clients { get; }
        IQueryable<IdentityResource> IdentityResources { get; }
        IQueryable<ApiResource> ApiResources { get; }
        Task AddClient(Client entity);
        Task AddIdentityResource(IdentityResource entity);
        Task AddApiResource(ApiResource entity);
    }
}