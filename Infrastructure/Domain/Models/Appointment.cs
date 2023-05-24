using System.ComponentModel.DataAnnotations.Schema;

namespace Capstonep2.Infrastructure.Domain.Models
{
    public class Appointment
    {
        public Guid? ID { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        
        
        public Guid? PatientID { get; set; }
        public Enums.Status Status { get; set; }
        public Enums.Visit Visit { get; set; }

        public string? FDescription { get; set; }
        public string? PDescription { get; set; }

        [ForeignKey("PatientID")]
        public Patient? Patient { get; set; }


        public ICollection<Symptom>? Symptoms { get; set; }
        public ICollection<Purpose>? Purposes { get; set; }

        public ICollection<Finding>? Findings { get; set; }
        public ICollection<Prescription>? Prescriptions { get; set; }
    }
}