using Mango.Web.DTO;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;
public class CouponController : Controller
{
    private readonly ICouponService _couponService;
    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }
    public async Task<IActionResult> CouponIndex()
    {
        List<CouponDto>? couponDtos = new();

        ResponseDto response = await _couponService.GetAllCouponAsync();

        if (response is not null && response.IsSuccess) 
        {
            couponDtos = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
        }

        return View(couponDtos);
    }
}
