using Microsoft.AspNetCore.Mvc;
using TicketSystem.Api.Enums;
using TicketSystem.Api.Models;

namespace TicketSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private static List<Ticket> tickets = new List<Ticket>
        {
            new Ticket { Id = 1, Title = "Drucker kaputt", Description = "Drucker im 2. OG blinkt rot.", Status = TicketStatusEnum.Offen, CreatedBy = "Max Mustermann" },
            new Ticket { Id = 2, Title = "Kein WLAN", Description = "Mein Laptop verbindet sich nicht.", Status = TicketStatusEnum.InBearbeitung, CreatedBy = "Luis Müller" }
        };

        [HttpGet]
        public ActionResult<List<Ticket>> GetAllTickets()
        {
            return Ok(tickets);
        }

        [HttpPost]
        public ActionResult<List<Ticket>> CreateTicket(Ticket newTicket)
        {
            newTicket.Id = tickets.Count > 0 ? tickets.Max(t => t.Id) + 1 : 1;
            
            tickets.Add(newTicket);
            return Ok(tickets);
        }
    }
}