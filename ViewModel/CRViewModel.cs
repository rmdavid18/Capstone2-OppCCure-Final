using Capstonep2.Infrastructure.Domain.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Capstonep2.ViewModel
{
    public class CRViewModel
    {
        public Infrastructure.Domain.Models.Enums.Status StatusEdit { get; set; }
        public Guid? ConsultationRecordID { get; set; }
        public Guid? PatientID { get; set; }
        public List<string>? SymptomsList { get; set; }
        public List<string>? PurposesList { get; set; }
        public List<string>? FindingsList { get; set; }
        public List<string>? PrescriptionsList { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? FindingID { get; set; }
        public string? FTags { get; set; }
        public string? FDescription { get; set; }
        public Guid? PrescriptionID { get; set; }
        public string? PTags { get; set; }
        public string? PDescription { get; set; }
        public Patient? Patient { get; set; }
        public List<Guid>? SymptomsEdit { get; set; }
        public List<Guid>? PurposesEdit {  get; set; }
        public Guid? ApptId { get; set; }
        public Guid ? EditId { get; set; }
        public string? FDesc { get; set; }
        public string? PDesc { get; set; }
        public List<Guid>? FList { get; set; }
        public List<Guid>? PList { get; set; }
        public Appointment? Appointment { get; set; }
        public List<Prescription>? Gamot { get; set; }
        public List<Prescription>? Prescriptions { get; set; }
        public List<Finding>? Findings { get; set; }
        public List<Symptom>? Sintomas { get; set; }
        public List<Purpose>? Dahilan { get; set; }
        public List<Appointment>? Appointments { get; set; }
        public List<Infrastructure.Domain.Models.Patient>? Patients { get; set; }
        public string? AppointmentId { get; set; }
        public List<ConsultationRecord>? ConsultationRecords { get; set; }
        public List<Symptom>? Symptoms { get; set; }
        public List<Purpose>? Purposes { get; set; }
        public Infrastructure.Domain.Models.Patient? Pasyente { get; set; }
    }
}
