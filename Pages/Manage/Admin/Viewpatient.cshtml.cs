using Capstonep2.Infrastructure.Domain;
using Capstonep2.Infrastructure.Domain.Models;
using Capstonep2.Infrastructure.Domain.Models.Enums;
using Capstonep2.Infrastructure.Domain.Security;
using Capstonep2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstonep2.Pages.Manage.Admin
{
    [Authorize(Roles = "admin")]
    public class ViewpatientModel : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDBContext _context;
        [BindProperty]
        public ViewModel View { get; set; }




        public ViewpatientModel(DefaultDBContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }
        public IActionResult OnGet(Guid? id = null, Guid? crid = null)
        {

            ViewData["id"] = id;
            var patient = _context?.Patients?.Where(a => a.ID == id).FirstOrDefault();
            if (patient != null)
            {


                ViewData["address"] = patient.Address;
                ViewData["birthdate"] = patient.BirthDate.ToString("dd/MM/yyyy");
                ViewData["firstname"] = patient.FirstName;
                ViewData["middlename"] = patient.MiddleName;
                ViewData["lastname"] = patient.LastName;
                ViewData["contact"] = patient.Contact;
                ViewData["gender"] = patient.Gender;



                var apptsRecords = _context.Appointments?.Where(a => a.PatientID == id).ToList();
                View.Appointments = apptsRecords;


                var consultations = _context?.ConsultationRecords?.ToList();
                View.ConsultationRecords = consultations;

                var findings = _context?.Findings?.ToList();
                var prescriptions = _context?.Prescriptions?.ToList();

                View.Findings = findings;
                View.Prescriptions = prescriptions;




            }

          View.PatientID = id;
            return Page();
        }

        public void OnPostRecord()
        {

            Guid? newId = Guid.NewGuid();
            DateTime? endTime = View.StartTime.Value.AddHours(1);
            Appointment appointment = new Appointment()
            {
                ID = newId,
                PatientID = View.PatientID,
                Visit = Infrastructure.Domain.Models.Enums.Visit.WalkIn,
                Status = Status.Completed,
                StartTime = View.StartTime,
                EndTime = endTime,
                PDescription = View.FDesc,
                FDescription = View.PDesc

            };
            _context?.Appointments?.Add(appointment);

            if (View.SList != null)
            {
                foreach (var symptom in View.SList)
                {


                    _context?.ApptSymptoms?.Add(new ApptSymptom()
                    {
                        Id = Guid.NewGuid(),
                        AppointmentId = newId,
                        SymptomId = symptom

                    });




                }

            }

            if (View.VList != null)
            {
                foreach (var purpose in View.VList)
                {


                    _context?.ApptPurposes?.Add(new ApptPurpose()
                    {
                        Id = Guid.NewGuid(),
                        AppointmetId = newId,
                        PurposeId = purpose

                    });




                }

            }




            if (View.PList != null)
            {
                foreach (var prescription in View.PList)
                {


                    _context?.ApptPrecriptions?.Add(new ApptPrecription()
                    {
                        Id = Guid.NewGuid(),
                        AppointmentId = newId,
                        PrescriptionId = prescription

                    });




                }

            }

            if (View.FList != null)
            {
                foreach (var finding in View.FList)
                {
                    _context?.ApptFindings.Add(new ApptFinding()
                    {
                        Id = Guid.NewGuid(),
                        AppointmentId = newId,
                        FindingId = finding,
                        
                    });

                }
            }

            _context?.SaveChanges();

            OnGet(View.PatientID);
        }
        public void OnPostSetappt()
        {

            Guid? newId = Guid.NewGuid();
            DateTime? endTime = View.StartTime.Value.AddHours(1);
            Appointment appointment = new Appointment()
            {
                ID = newId,
                PatientID = View.PatientID,
                Visit = Infrastructure.Domain.Models.Enums.Visit.Appointment,
                Status = Status.Pending,
                StartTime = View.StartTime,
                EndTime = endTime,
                PDescription = "",
                FDescription = ""

            };
            _context?.Appointments?.Add(appointment);

            if (View.SList != null)
            {
                foreach (var symptom in View.SList)
                {


                    _context?.ApptSymptoms?.Add(new ApptSymptom()
                    {
                        Id = Guid.NewGuid(),
                        AppointmentId = newId,
                        SymptomId = symptom

                    });




                }

            }

            if (View.VList != null)
            {
                foreach (var purpose in View.VList)
                {


                    _context?.ApptPurposes?.Add(new ApptPurpose()
                    {
                        Id = Guid.NewGuid(),
                        AppointmetId = newId,
                        PurposeId = purpose

                    });




                }

            }




           

         

            _context?.SaveChanges();

            OnGet(View.PatientID);
        }

        [HttpGet("purpose")]
        public JsonResult? OnGetPurpose(int pageIndex = 1, string? keyword = "", int pageSize = 10)
        {
            return new JsonResult(_context.Purposes!.Select(a => new LookupDto.Result()
            {
                Id = a.Id.ToString(),
                Text = a.Name ?? ""
            })
            .AsQueryable()
            .GetLookupPaged(pageIndex, pageSize));
        }

        [HttpGet("symptom")]
        public JsonResult? OnGetSymptom(int pageIndex = 1, string? keyword = "", int pageSize = 10)
        {
            return new JsonResult(_context.Symptoms!.Select(a => new LookupDto.Result()
            {
                Id = a.Id.ToString(),
                Text = a.Name ?? ""
            })
            .AsQueryable()
            .GetLookupPaged(pageIndex, pageSize));
        }
        [HttpGet("finding")]
        public JsonResult? OnGetFinding(int pageIndex = 1, string? keyword = "", int pageSize = 10)
        {
            return new JsonResult(_context.Findings!.Select(a => new LookupDto.Result()
            {
                Id = a.ID.ToString(),
                Text = a.FName ?? ""
            })
            .AsQueryable()
            .GetLookupPaged(pageIndex, pageSize));
        }
        [HttpGet("pres")]
        public JsonResult? OnGetPres(int pageIndex = 1, string? keyword = "", int pageSize = 10)
        {
            return new JsonResult(_context.Prescriptions!.Select(a => new LookupDto.Result()
            {
                Id = a.ID.ToString(),
                Text = a.GName ?? ""
            })
            .AsQueryable()
            .GetLookupPaged(pageIndex, pageSize));
        }

        [HttpGet("symptom")]
        public JsonResult? OnGetSymptomnew(int pageIndex = 1, string? keyword = "", int pageSize = 10)
        {
            return new JsonResult(_context.Symptoms!.Select(a => new LookupDto.Result()
            {
                Id = a.Id.ToString(),
                Text = a.Name ?? ""
            })
            .AsQueryable()
            .GetLookupPaged(pageIndex, pageSize));
        }
        [HttpGet("purpose")]
        public JsonResult? OnGetPurposenew(int pageIndex = 1, string? keyword = "", int pageSize = 10)
        {
            return new JsonResult(_context.Purposes!.Select(a => new LookupDto.Result()
            {
                Id = a.Id.ToString(),
                Text = a.Name ?? ""
            })
            .AsQueryable()
            .GetLookupPaged(pageIndex, pageSize));
        }



        public class ViewModel : UserViewModel
        {
            public Guid? PatientID { get; set; }
            public List<Guid>? SList { get; set; }
            public List<Guid>? VList { get; set; }
            public List<Guid>? FList { get; set; }
            public List<Guid>? PList { get; set; }
            public Guid? ApptId { get; set; }
            [ForeignKey("PatientID")]
            public Infrastructure.Domain.Models.Patient? Patient { get; set; }
            public List<Finding>? Findings { get; set; }
            public List<Prescription>? Prescriptions { get; set; }
            public List<ConsultationRecord>? ConsultationRecords { get; set; }
            public List<Appointment>? Appointments { get; set; }
            public List<Symptom>? Symptoms { get; set; }
            public List<Infrastructure.Domain.Models.Purpose>? Purposes { get; set; }

            public string? FTags { get; set; }
            public string? FDescription { get; set; }

            public string? FDesc { get; set; }
            public string? PDesc { get; set; }
            public string? PTags { get; set; }
            public string? PDescription { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
            public string? Symptom { get; set; }

            public Infrastructure.Domain.Models.Enums.Status Status { get; set; }
            public Infrastructure.Domain.Models.Enums.Purpose Purpose { get; set; }


        }

    }


}
