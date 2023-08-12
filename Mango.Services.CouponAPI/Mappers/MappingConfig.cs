using AutoMapper;
using Mango.Services.CouponAPI.DTO;
using Mango.Services.CouponAPI.Models;

namespace Mango.Services.CouponAPI.Mappers;
public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        MapperConfiguration mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<Coupon, CouponDto>().ReverseMap();
        });
        return mappingConfig;
    }
}
