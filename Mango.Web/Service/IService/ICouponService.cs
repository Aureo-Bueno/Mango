using Mango.Web.DTO;
using Mango.Web.Models;

namespace Mango.Web.Service.IService;
public interface ICouponService
{
    Task<ResponseDto?> GetCouponAsync(string couponCode);
    Task<ResponseDto?> GetAllCouponAsync();
    Task<ResponseDto?> GetCouponByIdAsync(int id);
    Task<ResponseDto?> CreateCouponAsync(CouponDto coupon);
    Task<ResponseDto?> UpdateCouponAsync(CouponDto coupon);
    Task<ResponseDto?> DeleteCouponAsync(int id);
}
