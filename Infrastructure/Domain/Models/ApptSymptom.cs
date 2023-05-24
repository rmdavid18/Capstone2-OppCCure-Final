namespace Capstonep2.Infrastructure.Domain.Models
{
    public class ApptSymptom
    {
        public Guid? Id { get; set; }
        public Guid? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
        public Guid? SymptomId { get; set; }
        public Symptom? Symptom { get; set; }
    }
}
