using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.DTO;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace Mango.Services.CouponAPI.Controllers;

[ApiController, Route("api/[controller]")]
public class CouponController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public CouponController(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Coupon> coupons = await _appDbContext.Coupon.ToListAsync();
        IEnumerable<CouponDto> response = _mapper.Map<IEnumerable<CouponDto>>(coupons);
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        Coupon coupon = await _appDbContext.Coupon.FirstOrDefaultAsync(x => x.Id == id);

        CouponDto response = _mapper.Map<CouponDto>(coupon);

        if (coupon is null)
            return NoContent();

        return Ok(response);
    }

    [HttpGet("GetByCode/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        Coupon coupon = await _appDbContext.Coupon.FirstOrDefaultAsync(x => x.CouponCode.ToLower() == code.ToLower());

        CouponDto response = _mapper.Map<CouponDto>(coupon);

        if (coupon is null)
            return NoContent();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            _appDbContext.Coupon.Add(coupon);
            await _appDbContext.SaveChangesAsync();
            
            return Created($"/id={coupon.Id}", coupon);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Erro ao processar a solicitação", error = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            _appDbContext.Coupon.Update(coupon);
            await _appDbContext.SaveChangesAsync();

            return Ok(coupon);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Erro ao processar a solicitação", error = ex.Message });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            Coupon coupon = await _appDbContext.Coupon.FirstAsync(x => x.Id == id);
            _appDbContext.Coupon.Remove(coupon);
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Erro ao processar a solicitação", error = ex.Message });
        }
    }
}
