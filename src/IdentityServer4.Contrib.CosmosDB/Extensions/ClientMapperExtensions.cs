using AutoMapper;
using IdentityServer4.Contrib.CosmosDB.Mappers;
using IdentityServer4.Models;

namespace IdentityServer4.Contrib.CosmosDB.Extensions
{
    /// <summary>
    ///     Extension methods to map IdentityServer4.Models.Client to
    ///     IdentityServer4.Contrib.CosmosDB.Entities.Client.
    /// </summary>
    public static class ClientMapperExtensions
    {
        static ClientMapperExtensions()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ClientMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        /// <summary>
        ///     Will map all data in IdentityServer4.Contrib.CosmosDB.Entities.Client to IdentityServer4.Models.Client.
        /// </summary>
        /// <param name="entity">The IdentityServer4.Contrib.CosmosDB.Entities.Client to gather data from.</param>
        /// <returns>An instance of IdentityServer4.Models.Client.</returns>
        public static Client ToModel(this Entities.Client entity)
        {
            return Mapper.Map<Client>(entity);
        }

        /// <summary>
        ///     Will map all data in IdentityServer4.Models.Client to IdentityServer4.Contrib.CosmosDB.Entities.Client.
        /// </summary>
        /// <param name="model">The IdentityServer4.Models.Client to gather data from.</param>
        /// <returns>An instance of IdentityServer4.Contrib.CosmosDB.Entities.Client.</returns>
        public static Entities.Client ToEntity(this Client model)
        {
            return Mapper.Map<Entities.Client>(model);
        }
    }
}