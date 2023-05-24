namespace Capstonep2.Infrastructure.Domain.Models
{
    public class ApptFinding
    {
        public Guid? Id { get; set; }
        public Guid? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
        public Guid? FindingId { get; set; }
        public Finding? Finding { get; set; }
    }
}
