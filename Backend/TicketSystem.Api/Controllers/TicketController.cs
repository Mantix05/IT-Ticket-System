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
            var tickets = __LoadTickets();
            return Ok(tickets);
        }

        [HttpPost]
        public ActionResult<List<Ticket>> CreateTicket(Ticket newTicket)
        {
            var tickets = __LoadTickets();

            newTicket.Id = tickets.Count > 0 ? tickets.Max(t => t.Id) + 1 : 1;
            
            tickets.Add(newTicket);
            
            __SaveTickets(tickets);

            return Ok(tickets);
        }

        [HttpPut("{ticket}")]
        public ActionResult<List<Ticket>> UpdateTicket(Ticket updatedTicket)
        {
            __UpdateTicket(updatedTicket);
            var tickets = __LoadTickets();
            return Ok(tickets);
        }

        [HttpDelete("{ticket}")]
        public ActionResult<List<Ticket>> DeleteTicket(int id)
        {
            __DeleteTicket(id);
            var tickets = __LoadTickets();
            return Ok(tickets);
        }

        #endregion

        #region Private Methods

         private List<Ticket> __LoadTickets()
        {
            if (!System.IO.File.Exists(_filePath))
            {
                return new List<Ticket>();
            }

            var json = System.IO.File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Ticket>>(json) ?? new List<Ticket>();
        }

        private void __SaveTickets(List<Ticket> tickets)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            
            var json = JsonSerializer.Serialize(tickets, options);
            System.IO.File.WriteAllText(_filePath, json);
        }

        private void __UpdateTicket(Ticket updatedTicket)
        {
            var json = System.IO.File.ReadAllText(_filePath);
            var options = new JsonSerializerOptions { WriteIndented = true };
            var existingTickets = JsonSerializer.Deserialize<List<Ticket>>(json) ?? new List<Ticket>();

            foreach (var ticket in existingTickets)
            {
                if (ticket.Id == updatedTicket.Id)
                {
                    var newTicketJson = JsonSerializer.Serialize(updatedTicket, options);
                    System.IO.File.WriteAllText(_filePath, newTicketJson);
                    break;
                }
            }

        }

        private void __DeleteTicket(int id)
        {
            var json = System.IO.File.ReadAllText(_filePath);
            var ticketList = JsonSerializer.Deserialize<List<Ticket>>(json) ?? new List<Ticket>();

            var itemToDelete = ticketList.FirstOrDefault(t => t.Id == id);
            if (itemToDelete != null)
            {
                ticketList.Remove(itemToDelete);
                __SaveTickets(ticketList);
            }
        }

        #endregion
        
    }
}