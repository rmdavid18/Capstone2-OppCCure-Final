namespace Capstonep2.Infrastructure.Domain.Models
{
    public class ApptPurpose
    {
        public Guid? Id { get; set; }
        public Guid? PurposeId { get; set; }
        public Guid? AppointmetId { get; set; }
        public Appointment? Appointment { get; set; }
        public Purpose? Purpose { get; set; }
    }
}
