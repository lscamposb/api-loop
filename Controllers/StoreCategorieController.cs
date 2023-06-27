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
    public class StoreCategorieController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly StoreCategorieService _storeCategorieService;

        public StoreCategorieController(IConfiguration config, StoreCategorieService storeCategorieService)
        {
            _configuration = config;
            this._storeCategorieService = storeCategorieService;
        }

        // GET api/<StoreCategorieController>
        [HttpGet("{language}")]
        public ActionResult<List<StoreCategories>> Get(string language)
        {
            try
            {
                List<StoreCategories> store = _storeCategorieService.Get();

                return Ok(store);
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // GET api/<StoreCategorieController>/1
        [HttpGet("{id}/{language}")]
        public ActionResult<StoreCategories> Get(int id, string language)
        {
            try
            {
                StoreCategories store = _storeCategorieService.Get(id);

                if (store == null)
                    return BadRequest(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_CATEGORIA_TIENDA, language), (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_USUARIO_SIN_REGISTRAR, language)));

                return Ok(store);
            } 
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // POST api/<StoreCategorieController>
        [HttpPost]
        public ActionResult<MensajeApi> Post(StoreCategoriesDTO store, string language)
        {
            try
            {
                StoreCategories lStore = new StoreCategories();
                lStore.id = _storeCategorieService.ObtenerUltimoRegistro();
                lStore.name = store.name;

                _storeCategorieService.Post(lStore);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_CATEGORIA_TIENDA, language), (int)ExceptionCodeEnum.Codigo200, Language.GetMensage(IdiomaCodeEnum.MENSAJE_GENERICO_REGISTRAR, language)));
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // PUT api/<StoreCategorieController>/5
        [HttpPut]
        public ActionResult<MensajeApi> Put(StoreCategoriesDTO store, string language)
        {
            try
            {
                StoreCategories lStore = _storeCategorieService.Get(store.id);

                if (lStore == null)
                    return BadRequest(new MensajeApi(IdiomaCodeEnum.TITULO_CATEGORIA_TIENDA, (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_CATEGORIA_TIENDA_SIN_REGISTRAR, language)));

                lStore.name = store.name;

                _storeCategorieService.Update(lStore);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_CATEGORIA_TIENDA, language), (int)ExceptionCodeEnum.Codigo200, Language.GetMensage(IdiomaCodeEnum.MENSAJE_GENERICO_ACTUALIZAR, language)));
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // DELETE api/<StoreCategorieController>/5
        [HttpDelete("{id}/{language}")]
        public ActionResult<MensajeApi> Delete(int id, string language)
        {
            try
            {
                StoreCategories lStore = _storeCategorieService.Get(id);

                if (lStore == null)
                    return BadRequest(new MensajeApi(IdiomaCodeEnum.TITULO_CATEGORIA_TIENDA, (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_CATEGORIA_TIENDA_SIN_REGISTRAR, language)));

                _storeCategorieService.Delete(id);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_CATEGORIA_TIENDA, language), (int)ExceptionCodeEnum.Codigo200, Language.GetMensage(IdiomaCodeEnum.MENSAJE_GENERICO_ELIMINAR, language)));
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }
    }
}
