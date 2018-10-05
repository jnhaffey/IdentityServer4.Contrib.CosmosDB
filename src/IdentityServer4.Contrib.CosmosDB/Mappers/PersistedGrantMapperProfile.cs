using AutoMapper;
using IdentityServer4.Contrib.CosmosDB.Entities;

namespace IdentityServer4.Contrib.CosmosDB.Mappers
{
    /// <inheritdoc />
    /// <summary>
    ///     AutoMapper Config for PersistedGrant
    ///     Between Model and Entity
    ///     <seealso cref="!:https://github.com/AutoMapper/AutoMapper/wiki/Configuration">
    ///     </seealso>
    /// </summary>
    public class PersistedGrantMapperProfile : Profile
    {
        /// <summary>
        ///     <see cref="PersistedGrantMapperProfile">
        ///     </see>
        /// </summary>
        public PersistedGrantMapperProfile()
        {
            // entity to model
            CreateMap<PersistedGrant, Models.PersistedGrant>(MemberList.Destination);

            // model to entity
            CreateMap<Models.PersistedGrant, PersistedGrant>(MemberList.Source);
        }
    }
}