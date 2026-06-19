using Microsoft.AspNetCore.Mvc;
using System.Text.Json; // NEU: Für die JSON-Umwandlung (Serialisierung)
using System.IO;        // NEU: Für das Lesen/Schreiben von Dateien auf der Festplatte
using TicketSystem.Api.Models;
using TicketSystem.Api.Enums; 

namespace TicketSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        #region Readonly

        private readonly string _filePath = "tickets.json";

        #endregion

        #region Public Methods

        [HttpGet]
        public ActionResult<List<Ticket>> GetAllTickets()
        {
            var tickets = LoadTickets();
            return Ok(tickets);
        }

        [HttpPost]
        public ActionResult<List<Ticket>> CreateTicket(Ticket newTicket)
        {
            var tickets = LoadTickets();

            newTicket.Id = tickets.Count > 0 ? tickets.Max(t => t.Id) + 1 : 1;
            
            tickets.Add(newTicket);
            
            SaveTickets(tickets);

            return Ok(tickets);
        }

        #endregion

        #region Private Methods

         private List<Ticket> LoadTickets()
        {
            if (!System.IO.File.Exists(_filePath))
            {
                return new List<Ticket>();
            }

            var json = System.IO.File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Ticket>>(json) ?? new List<Ticket>();
        }

        private void SaveTickets(List<Ticket> tickets)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            
            var json = JsonSerializer.Serialize(tickets, options);
            System.IO.File.WriteAllText(_filePath, json);
        }

        #endregion
        
    }
}