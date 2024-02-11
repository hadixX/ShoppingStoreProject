using AutoMapper;
using ShoppingStore.Models;
using ShoppingStore.Models.Dtos;

namespace ShoppingStore.Common.Profiles
{
    public class AutoMapperProfile: Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<ItemsDto,Items>().ReverseMap();
        }
    }
}
