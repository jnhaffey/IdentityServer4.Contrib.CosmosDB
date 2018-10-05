using AutoMapper;
using IdentityServer4.Contrib.CosmosDB.Mappers;
using IdentityServer4.Models;

namespace IdentityServer4.Contrib.CosmosDB.Extensions
{
    public static class ApiResourceMapperExtensions
    {
        static ApiResourceMapperExtensions()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ApiResourceMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static ApiResource ToModel(this Entities.ApiResource resource)
        {
            return resource == null ? null : Mapper.Map<ApiResource>(resource);
        }

        public static Entities.ApiResource ToEntity(this ApiResource resource)
        {
            return resource == null ? null : Mapper.Map<Entities.ApiResource>(resource);
        }
    }
}