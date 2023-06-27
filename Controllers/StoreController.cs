using LoopApi.DTO;
using LoopApi.Enum;
using LoopApi.Models;
using LoopApi.Services;
using LoopApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoopApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StoreController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly StoreService _storeService;

        public StoreController(IConfiguration config, StoreService storeService)
        {
            _configuration = config;
            _storeService = storeService;
        }

        // GET api/<StoreCategorieController>
        [HttpGet("{language}")]
        public ActionResult<List<StoreDTO>> Get(string language)
        {
            try
            {
                List<Store> store = _storeService.Get();

                List<StoreDTO> lResult = new List<StoreDTO>();

                StoreDTO lStore = null;

                foreach (Store item in store)
                {
                    lStore = new StoreDTO();
                    lStore.id = item.id;
                    lStore.name = item.name;
                    lStore.color = item.color;
                    lStore.urlWebPage = item.url_web_page;
                    lStore.points = item.points;
                    lStore.urlImage = item.url_image;

                    lResult.Add(lStore);
                }

                return Ok(lResult);
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }
    }
}
