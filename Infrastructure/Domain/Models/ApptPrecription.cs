namespace Capstonep2.Infrastructure.Domain.Models
{
    public class ApptPrecription
    {
        public Guid? Id { get; set; }
        public Appointment? Appointment { get; set; }
        public Guid? AppointmentId { get; set; }
        public Guid? PrescriptionId { get; set; }
        public Prescription? Prescription { get; set; }
    }
}
