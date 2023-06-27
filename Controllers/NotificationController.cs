using LoopApi.Enum;
using LoopApi.Models;
using LoopApi.Services;
using LoopApi.Util;
using Microsoft.AspNetCore.Mvc;

namespace LoopApi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class NotificationController : Controller
    {
        public IConfiguration _configuration;
        private readonly NotificationService _notificationService;

        public NotificationController(IConfiguration config, NotificationService notificationService)
        {
            _configuration = config;
            _notificationService = notificationService;
        }

        // GET api/<StoreCategorieController>
        [HttpGet("{isRead}/{language}")]
        public ActionResult<List<Notification>> Get(int isRead, string language)
        {
            try
            {
                List<Notification> notification = _notificationService.Get(isRead);               

                return Ok(notification);
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }
    }
}
