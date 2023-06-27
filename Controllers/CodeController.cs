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
    public class CodeController : ControllerBase
    {
        private readonly CodeService _codeService;

        public CodeController(CodeService codeService)
        {
            _codeService = codeService;
        }

        // GET api/<AuthorityController>/es
        [HttpGet("{language}")]
        public ActionResult<List<Code>> Get(string language)
        {
            try
            {
                List<Code> code = _codeService.Get();

                return Ok(code);
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // GET api/<AuthorityController>/1/es
        [HttpGet("{id}/{language}")]
        public ActionResult<Code> Get(long id, string language)
        {
            try
            {
                Code code = _codeService.Get(id);

                if (code == null)
                    return BadRequest(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_CODIGO, language), (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_CODIGO_SIN_REGISTRAR, language)));

                return Ok(code);
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // POST api/<AuthorityController>
        [HttpPost]
        public ActionResult<MensajeApi> Post(CodeDTO code, string language)
        {
            try
            {
                Code lCode = new Code();
                lCode.id = _codeService.ObtenerUltimoRegistro();
                lCode.coupon_id = code.couponId;
                lCode.code = code.code;
                lCode.is_active = code.isActive;
                lCode.created_date = code.createdDate;
                lCode.updated_date = code.updatedDate;

                _codeService.Create(lCode);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_CODIGO, language), (int)ExceptionCodeEnum.Codigo200, Language.GetMensage(IdiomaCodeEnum.MENSAJE_GENERICO_REGISTRAR, language)));
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // PUT api/<AuthorityController>/es
        [HttpPut]
        public ActionResult<MensajeApi> Put(CodeDTO code, string language)
        {
            try
            {
                Code lCode = _codeService.Get(code.id);

                if (lCode == null)
                    return BadRequest(new MensajeApi(IdiomaCodeEnum.TITULO_CODIGO, (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_CODIGO_SIN_REGISTRAR, language)));

                lCode.coupon_id = code.couponId;
                lCode.code = code.code;
                lCode.is_active = code.isActive;
                lCode.created_date = code.createdDate;
                lCode.updated_date = code.updatedDate;

                _codeService.Update(lCode);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_CODIGO, language), (int)ExceptionCodeEnum.Codigo200, Language.GetMensage(IdiomaCodeEnum.MENSAJE_GENERICO_ACTUALIZAR, language)));
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // DELETE api/<AuthorityController>/5/es
        [HttpDelete("{id}/{language}")]
        public ActionResult<MensajeApi> Delete(long id, string language)
        {
            try
            {
                Code lCode = _codeService.Get(id);

                if (lCode == null)
                    return BadRequest(new MensajeApi(IdiomaCodeEnum.TITULO_CODIGO, (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_AUTORIDAD_SIN_REGISTRAR, language)));

                _codeService.Delete(id);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_CODIGO, language), (int)ExceptionCodeEnum.Codigo200, Language.GetMensage(IdiomaCodeEnum.MENSAJE_GENERICO_ELIMINAR, language)));
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }
    }
}
