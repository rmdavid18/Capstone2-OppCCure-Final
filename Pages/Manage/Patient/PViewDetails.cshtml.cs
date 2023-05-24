using Capstonep2.Infrastructure.Domain;
using Capstonep2.Infrastructure.Domain.Models;
using Capstonep2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Capstonep2.Pages.Manage.Patient
{
    [Authorize(Roles = "patient")]
    public class PViewDetailsModel : PageModel
    {

        private ILogger<System.Index> _logger;
        private DefaultDBContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public PViewDetailsModel(DefaultDBContext context, ILogger<System.Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }



        public void OnGet(Guid? ID = null)
        {
            var appointment = _context?.Appointments?.Where(a => a.ID == ID).Include(a => a.Patient).FirstOrDefault();
            if (appointment != null) { View.Appointment = appointment; }

            var apptSymptoms = _context?.ApptSymptoms?.Where(a => a.AppointmentId == ID).ToList(); //Get ApptSymptomList
            var apptPurposes = _context?.ApptPurposes?.Where(a => a.AppointmetId == ID).ToList();
            var apptFindings = _context?.ApptFindings?.Where(a => a.AppointmentId == ID).ToList();
            var apptPrescriptions = _context?.ApptPrecriptions?.Where(a => a.AppointmentId == ID).ToList();

            List<string?> symptomList = new List<string?>(); //Declare symptom list
            List<string?> purposeList = new List<string?>();
            List<string?> findingList = new List<string?>();
            List<string?> prescriptionList = new List<string?>();


            Symptom symptom = new Symptom();
            Purpose purpose = new Purpose();
            Finding finding = new Finding();
            Prescription prescription = new Prescription();



            if (appointment != null)
            {
                View.Appointment = appointment;


                foreach (var item in apptSymptoms) //For each matching symptom
                {
                    symptom = _context?.Symptoms?.Where(a => a.Id == item.SymptomId).FirstOrDefault(); //Get Symptoms
                    if (symptom != null)
                    {
                        symptomList.Add(symptom.Name);
                    }
                }

                foreach (var item in apptPurposes)
                {
                    purpose = _context?.Purposes?.Where(a => a.Id == item.PurposeId).FirstOrDefault();
                    if (purpose != null)
                    {
                        purposeList.Add(purpose.Name);
                    }

                }

                foreach (var item in apptFindings)
                {
                    finding = _context?.Findings?.Where(a => a.ID == item.FindingId).FirstOrDefault();
                    if (finding != null)
                    {
                        findingList.Add(finding.FName);
                    }
                }

                foreach (var item in apptPrescriptions)
                {
                    prescription = _context?.Prescriptions?.Where(a => a.ID == item.PrescriptionId).FirstOrDefault();
                    if (prescription != null)
                    {
                        prescriptionList.Add(prescription.GName);
                    }
                }


                View.SymptomsList = symptomList;
                View.PurposesList = purposeList;
                View.FindingsList = findingList;
                View.PrescriptionsList = prescriptionList;

            }
            View.ApptId = ID;
        }

        public void OnPostApptsedit()
        {
            
            var appointment = _context?.Appointments?.Where(a => a.ID == View.ApptId).FirstOrDefault();
            if (appointment != null)
            {
                var apptSymptoms = _context?.ApptSymptoms.Where(a=> a.AppointmentId == View.ApptId).ToList();
                var apptPurposes = _context?.ApptPurposes.Where(a => a.AppointmetId == View.ApptId).ToList();

                _context?.ApptSymptoms.RemoveRange(apptSymptoms);
                _context?.ApptPurposes.RemoveRange(apptPurposes);
                if (View.PurposesEdit != null)
                {
                    foreach (var purpose in View.PurposesEdit)
                    {


                        _context?.ApptPurposes?.Add(new ApptPurpose()
                        {
                            Id = Guid.NewGuid(),
                            AppointmetId = View.ApptId,
                            PurposeId = purpose
                        });




                    }

                }


                if (View.PurposesEdit != null)
                {
                    foreach (var symptom in View.SymptomsEdit)
                    {
                        _context.ApptSymptoms?.Add(new ApptSymptom()
                        {
                            Id = Guid.NewGuid(),
                            AppointmentId = View.ApptId,
                            SymptomId = symptom
                        });


                    }
                }
                _context.SaveChanges();
                OnGet(View.ApptId);
            }

        }
        public void OnPostCanceled()
        {
            var status = _context?.Appointments?.FirstOrDefault(a=> a.ID == View.ApptId);
            if (status != null)
            {
                status.Status = Infrastructure.Domain.Models.Enums.Status.Cancelled;
                _context?.Appointments.Update(status);
            }
            _context.SaveChanges();
            OnGet(View.ApptId);

        }

        [HttpGet("purposeedit")]
        public JsonResult? OnGetPurposeedit(int pageIndex = 1, string? keyword = "", int pageSize = 10)
        {
            return new JsonResult(_context.Purposes!.Select(a => new LookupDto.Result()
            {
                Id = a.Id.ToString(),
                Text = a.Name ?? ""
            })
            .AsQueryable()
            .GetLookupPaged(pageIndex, pageSize));
        }

        [HttpGet("symptomedit")]
        public JsonResult? OnGetSymptomedit(int pageIndex = 1, string? keyword = "", int pageSize = 10)
        {
            return new JsonResult(_context.Symptoms!.Select(a => new LookupDto.Result()
            {
                Id = a.Id.ToString(),
                Text = a.Name ?? ""
            })
            .AsQueryable()
            .GetLookupPaged(pageIndex, pageSize));
        }

        public class ViewModel : CRViewModel
        {
            public Appointment Appointment { get; set; }
            public List<Symptom>? Symptoms { get; set; }
            public List <Infrastructure.Domain.Models.Patient>? Patient { get; set; }
            public List<Purpose>? Purposes { get; set; }
            public List<Appointment>? Appointments { get; set; }
            public string? AppointmentId { get; set; }
            public List<Finding>? Findings { get; set; }
            public List<Prescription>? Prescriptions { get; set; }
            public List<ConsultationRecord>? ConsultationRecords { get; set; }
            public Infrastructure.Domain.Models.Patient? Pasyente { get; set; }
            
        }
    }
}
