using AutoMapper;
using IdentityServer4.Contrib.CosmosDB.Mappers;
using IdentityServer4.Models;

namespace IdentityServer4.Contrib.CosmosDB.Extensions
{
    public static class IdentityResourceMapperExtensions
    {
        static IdentityResourceMapperExtensions()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<IdentityResourceMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static IdentityResource ToModel(this Entities.IdentityResource resource)
        {
            return resource == null ? null : Mapper.Map<IdentityResource>(resource);
        }

        public static Entities.IdentityResource ToEntity(this IdentityResource resource)
        {
            return resource == null ? null : Mapper.Map<Entities.IdentityResource>(resource);
        }
    }
}