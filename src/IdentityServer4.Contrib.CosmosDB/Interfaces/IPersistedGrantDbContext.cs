using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace IdentityServer4.Contrib.CosmosDB.Interfaces
{
    public interface IPersistedGrantDbContext : IDisposable
    {
        IQueryable<PersistedGrant> PersistedGrants { get; }

        Task Add(PersistedGrant entity);

        Task Update(PersistedGrant entity);

        Task Remove(PersistedGrant entity);

        Task RemoveExpired();
    }
}