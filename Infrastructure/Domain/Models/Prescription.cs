using System.ComponentModel.DataAnnotations.Schema;

namespace Capstonep2.Infrastructure.Domain.Models
{
    public class Prescription
    {
        public Guid? ID { get; set; }
        public string? GName { get; set; }
        
        public Guid? ConsultationRecordID { get; set; }

        [ForeignKey("ConsultationRecordID")]
        public ConsultationRecord? Consultation { get; set; }
    }
}
