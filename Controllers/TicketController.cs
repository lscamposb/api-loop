using LoopApi.DTO;
using LoopApi.Enum;
using LoopApi.Models;
using LoopApi.Services;
using LoopApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoopApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: api/<TicketController>/es
        [HttpGet("{language}")]
        public ActionResult<List<Ticket>> Get(string language)
        {
            try
            {
                List<Ticket> code = _ticketService.Get();

                return Ok(code);
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // GET api/<TicketController>/5/es
        [HttpGet("{id}/{language}")]
        public ActionResult<Ticket> Get(long id, string language)
        {
            try
            {
                Ticket ticket = _ticketService.Get(id);

                if (ticket == null)
                    return BadRequest(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_TICKET, language), (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_CODIGO_SIN_REGISTRAR, language)));

                return Ok(ticket);
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // POST api/<TicketController>
        [HttpPost]
        public ActionResult<Ticket> Post(TicketDTO ticket, string language)
        {
            try
            {
                Ticket lTicket = new Ticket();
                lTicket.id = _ticketService.ObtenerUltimoRegistro();
                lTicket.ticket_number = ticket.ticketNumber;
                lTicket.machine_number = ticket.machineNumber;
                lTicket.is_enable = ticket.isEnable;
                lTicket.created_date = ticket.createdDate;
                lTicket.updated_date = ticket.updatedDate;
                lTicket.is_applied = ticket.isApplied;
                lTicket.class_ticket = ticket.classTicket;

                _ticketService.Create(lTicket);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_TICKET, language), (int)ExceptionCodeEnum.Codigo200, Language.GetMensage(IdiomaCodeEnum.MENSAJE_GENERICO_REGISTRAR, language)));
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // PUT api/<TicketController>/5
        [HttpPut]
        public ActionResult Put(TicketDTO ticket, string language)
        {
            try
            {
                Ticket lTicket = _ticketService.Get(ticket.id);

                if (lTicket == null)
                    return BadRequest(new MensajeApi(IdiomaCodeEnum.TITULO_TICKET, (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_TICKET_SIN_REGISTRAR, language)));

                lTicket.ticket_number = ticket.ticketNumber;
                lTicket.machine_number = ticket.machineNumber;
                lTicket.is_enable = ticket.isEnable;
                lTicket.updated_date = ticket.updatedDate;
                lTicket.is_applied = ticket.isApplied;
                lTicket.class_ticket = ticket.classTicket;

                _ticketService.Update(lTicket);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_TICKET, language), (int)ExceptionCodeEnum.Codigo200, Language.GetMensage(IdiomaCodeEnum.MENSAJE_GENERICO_ACTUALIZAR, language)));
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}/{language}")]
        public ActionResult Delete(long id, string language)
        {
            try
            {
                Ticket ticket = _ticketService.Get(id);

                if (ticket == null)
                    return BadRequest(new MensajeApi(IdiomaCodeEnum.TITULO_TICKET, (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_TICKET_SIN_REGISTRAR, language)));

                _ticketService.Delete(id);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_TICKET, language), (int)ExceptionCodeEnum.Codigo200, Language.GetMensage(IdiomaCodeEnum.MENSAJE_GENERICO_ELIMINAR, language)));
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }
    }
}
