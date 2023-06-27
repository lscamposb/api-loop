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
    public class AuthorityController : ControllerBase
    {
        private readonly AuthorityCategoriesService _authorityService;

        public AuthorityController(AuthorityCategoriesService _authorityService)
        {
            this._authorityService = _authorityService;
        }

        // GET api/<AuthorityController>
        [HttpGet("{language}")]
        public ActionResult<List<AuthorityCategories>> Get(string language)
        {
            try
            {
                List<AuthorityCategories> authority = _authorityService.Get();

                return Ok(authority);
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }
        
        // GET api/<AuthorityController>/1/es
        [HttpGet("{id}/{language}")]
        public ActionResult<AuthorityCategories> Get(long id, string language)
        {
            try
            {
                AuthorityCategories authority = _authorityService.Get(id);

                if (authority == null)
                    return BadRequest(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_AUTORIDAD, language), (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_AUTORIDAD_SIN_REGISTRAR, language)));

                return Ok(authority);
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }
        
        // POST api/<AuthorityController>
        [HttpPost]
        public ActionResult<MensajeApi> Post(AuthorityDTO authority, string language)
        {
            try
            {
                AuthorityCategories lAuthority = new AuthorityCategories();
                lAuthority.id = _authorityService.ObtenerUltimoRegistro();
                lAuthority.name = authority.name;

                _authorityService.Post(lAuthority);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_AUTORIDAD, language), (int)ExceptionCodeEnum.Codigo200, Language.GetMensage(IdiomaCodeEnum.MENSAJE_GENERICO_REGISTRAR, language)));
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // PUT api/<AuthorityController>/5/es
        [HttpPut]
        public ActionResult<MensajeApi> Put(AuthorityDTO authority, string language)
        {
            try
            {
                AuthorityCategories lAuthority = _authorityService.Get(authority.id);

                if (lAuthority == null)
                    return BadRequest(new MensajeApi(IdiomaCodeEnum.TITULO_AUTORIDAD, (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_AUTORIDAD_SIN_REGISTRAR, language)));

                lAuthority.name = authority.name;

                _authorityService.Update(lAuthority);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_AUTORIDAD, language), (int)ExceptionCodeEnum.Codigo200, Language.GetMensage(IdiomaCodeEnum.MENSAJE_GENERICO_ACTUALIZAR, language)));
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
                AuthorityCategories lAuthority = _authorityService.Get(id);

                if (lAuthority == null)
                    return BadRequest(new MensajeApi(IdiomaCodeEnum.TITULO_AUTORIDAD, (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_AUTORIDAD_SIN_REGISTRAR, language)));

                _authorityService.Delete(id);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_AUTORIDAD, language), (int)ExceptionCodeEnum.Codigo200, Language.GetMensage(IdiomaCodeEnum.MENSAJE_GENERICO_ELIMINAR, language)));
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }
    }
}
