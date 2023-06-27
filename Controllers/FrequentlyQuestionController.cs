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
    public class FrequentlyQuestionController : ControllerBase
    {
        private readonly FrequentlyQuestionService _frequentlyQuestionService;

        public FrequentlyQuestionController(FrequentlyQuestionService frequentlyQuestionService)
        {
            _frequentlyQuestionService = frequentlyQuestionService;
        }

        [HttpGet("{language}")]
        public ActionResult<List<FrequentlyQuestion>> Get(string language)
        {
            try
            {
                List<FrequentlyQuestion> code = _frequentlyQuestionService.Get();

                return Ok(code);
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }
    }
}
