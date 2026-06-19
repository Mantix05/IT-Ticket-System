namespace TicketSystem.Api.Models
{
    using TicketSystem.Api.Enums;
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatusEnum Status { get; set; } = TicketStatusEnum.Offen;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = null;
        public string CreatedBy { get; set; } = string.Empty;
    }
}