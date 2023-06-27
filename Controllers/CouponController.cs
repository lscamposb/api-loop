using LoopApi.DTO;
using LoopApi.Enum;
using LoopApi.Models;
using LoopApi.Services;
using LoopApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoopApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CouponController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly CouponService _couponService;

        public CouponController(IConfiguration config, CouponService couponService)
        {
            _configuration = config;
            _couponService = couponService;
        }

        // GET api/<CouponController>/es
        [HttpGet("{language}")]
        public ActionResult<List<Coupon>> Get(string language)
        {
            try
            {
                List<Coupon> coupon = _couponService.Get();

                return Ok(coupon);
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // GET api/<CouponController>/1/es
        [HttpGet("{id}/{language}")]
        public ActionResult<Coupon> Get(long id, string language)
        {
            try
            {
                Coupon coupon = _couponService.Get(id);

                if (coupon == null)
                    return BadRequest(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_CODIGO, language), (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_CODIGO_SIN_REGISTRAR, language)));

                return Ok(coupon);
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // GET api/<CouponController>/1/es
        [HttpGet("[action]")]
        public ActionResult<Coupon> GetStore(long idStore, string language)
        {
            try
            {
                List<Coupon> coupon = _couponService.GetStore(idStore);

                return Ok(coupon);
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }
    }
}
