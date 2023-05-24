using Capstonep2.Infrastructure.Domain.Security;
using Capstonep2.Infrastructure.Domain;
using Capstonep2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Capstonep2.Infrastructure.Domain.Models;
using System;
using Capstonep2.Infrastructure.Domain.Models.Enums;
using static Capstonep2.Pages.Manage.Patient.DashboardModel.ViewModel;

namespace Capstonep2.Pages.Manage.Patient
{
    [Authorize(Roles = "patient")]
    public class DashboardModel : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDBContext _context;
        [BindProperty]
        public ViewModel View { get; set; }


        public DashboardModel(DefaultDBContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();

        }


        public IActionResult OnGet(int? pageIndex = 1, int? pageSize = 10, Status? status = null, DateTime? date = null)
        {

            Guid? userId = User.Id();
            var user = _context?.Users?.Where(a => a.ID == userId)
                .Select(a => new ViewModel()
                {

                    Address = a.Address,
                    BirthDate = a.BirthDate,
                    Email = a.Email,
                    FirstName = a.FirstName,
                    Gender = a.Gender,
                    LastName = a.LastName,
                    MiddleName = a.MiddleName,
                    PatientID = a.PatientID
                }).FirstOrDefault();

            Guid? patientId = user.PatientID;
            var patientConsultation = _context?.ConsultationRecords?.Where(a => a.PatientID == patientId).ToList();
            View.ConsultationRecords = patientConsultation;

            var findings = _context?.Findings.ToList();
            var prescriptions = _context?.Prescriptions.ToList();

            View.Findings = findings;
            View.Prescriptions = prescriptions;



            var skip = (int)((pageIndex - 1) * pageSize);

            var query = _context.Appointments.Where(a => a.PatientID == patientId).Include(a=> a.Patient).AsQueryable();

            var totalRows = query.Count();

       
            if (status != null)
            {
                query = query.Where(a => a.Status == status);
            }

            if (date != null)
            {
                query = query.Where(a => a.EndTime > date && a.EndTime < date.Value.AddDays(1));
            }
            var appointments = query
            .Skip(skip)
                            .Take((int)pageSize)
                            .ToList();

            View.Appointments = new Paged<Appointment>()
            {
                Items = appointments,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRows
                
            };









            ViewData["address"] = user.Address;
            ViewData["birthdate"] = user.BirthDate.ToString("dd/MM/yyyy");
            ViewData["email"] = user.Email;
            ViewData["firstname"] = user.FirstName;
            ViewData["middlename"] = user.MiddleName;
            ViewData["lastname"] = user.LastName;
            ViewData["gender"] = user.Gender;
            ViewData["PatientID"] = user.PatientID.ToString();

            return Page();
        }



        public IActionResult OnPostChangeProfile()
        {
            if (string.IsNullOrEmpty(View.FirstName))
            {
                ModelState.AddModelError("", "Role name cannot be blank.");
                return Page();
            }
            if (string.IsNullOrEmpty(View.MiddleName))
            {
                ModelState.AddModelError("", "Role name cannot be blank.");
                return Page();
            }
            if (string.IsNullOrEmpty(View.LastName))
            {
                ModelState.AddModelError("", "Role name cannot be blank.");
                return Page();
            }

            if (string.IsNullOrEmpty(View.Address))
            {
                ModelState.AddModelError("", "Description cannot be blank.");
                return Page();
            }


            var existingPatient = _context?.Patients?.FirstOrDefault(a =>
                    a.FirstName.ToLower() == View.FirstName.ToLower() &&
                    a.LastName.ToLower() == View.LastName.ToLower() &&
                    a.MiddleName.ToLower() == View.MiddleName.ToLower() &&
                    a.Address.ToLower() == View.Address.ToLower()






            );

            if (existingPatient != null)
            {
                ModelState.AddModelError("", "Patient is already existing.");
                return Page();
            }

            var user = _context?.Users?.FirstOrDefault(a => a.ID == User.Id());

            var patient = _context?.Patients?.FirstOrDefault(a => a.ID == user.PatientID);

            if (patient != null)
            {

                patient.FirstName = View.FirstName;
                patient.MiddleName = View.MiddleName;
                patient.LastName = View.LastName;
                patient.Address = View.Address;
                user.FirstName = View.FirstName;
                user.MiddleName = View.MiddleName;
                user.LastName = View.LastName;
                user.Address = View.Address;




                _context?.Patients?.Update(patient);
                _context?.Users?.Update(user);
                _context?.SaveChanges();

                return RedirectPermanent("~/Manage/Patient/Dashboard");
            }

            return Page();

        }


        public IActionResult OnPostChangePass()
        {

            if (string.IsNullOrEmpty(View.CurrentPass))
            {
                ModelState.AddModelError("", "Field Required");
                return Page();
            }

            if (string.IsNullOrEmpty(View.NewPass))
            {
                ModelState.AddModelError("", "Field Required");
                return Page();
            }

            if (string.IsNullOrEmpty(View.RetypedPassword))
            {
                ModelState.AddModelError("", "Field Required");
                return Page();
            }

            if (View.NewPass != View.RetypedPassword)
            {
                ModelState.AddModelError("", "Passwords are not the same");
                return Page();
            }


            var passwordInfo = _context?.UserLogins?.FirstOrDefault(a => a.UserID == User.Id() && a.Key.ToLower() == "password");

            if (passwordInfo != null)
            {
                if (BCrypt.Net.BCrypt.EnhancedVerify(View.CurrentPass, passwordInfo.Value))
                {
                    var userRole = _context?.UserRoles?.Include(a => a.Role)!.FirstOrDefault(a => a.UserID == User.Id());

                    passwordInfo.Value = BCrypt.Net.BCrypt.EnhancedHashPassword(View.NewPass);
                    _context?.UserLogins?.Update(passwordInfo);
                    _context?.SaveChanges();

                    if (userRole!.Role!.Name.ToLower() == "admin")
                    {
                        return RedirectPermanent("/manage/admin/dashboard");
                    }
                    else
                    {
                        return RedirectPermanent("/manage/patient/dashboard");
                    }
                }
            }
            return RedirectPermanent("/manage/patient/dashboard");
        }

        public IActionResult OnPostAppointment()
        {
            Guid? userId = User.Id();
            var user = _context?.Users?.Where(a => a.ID == userId).FirstOrDefault();

            Guid? patientId = user.PatientID;



            if (DateTime.MinValue >= View.StartTime)
            {
                ModelState.AddModelError("", "Start cannot be blank.");
                return Page();
            }
            if (DateTime.MinValue >= View.EndTime)
            {
                ModelState.AddModelError("", "End cannot be blank.");
                return Page();
            }

            DateTime? endTime = View.StartTime.Value.AddHours(1);

            View.ID = Guid.NewGuid();
            Appointment appointment = new Appointment()
            {
                ID = View.ID,
                PatientID = patientId,
                Visit = Infrastructure.Domain.Models.Enums.Visit.Appointment,
                StartTime = View.StartTime,
                EndTime = endTime,
                PDescription = "",
                FDescription = ""

            };

           
            _context?.Appointments?.Add(appointment);
            if (View.PurposeList != null)
            {
                foreach (var purpose in View.PurposeList)
                {


                    _context?.ApptPurposes?.Add(new ApptPurpose()
                    {
                        Id = Guid.NewGuid(),
                        AppointmetId = appointment.ID,
                        PurposeId = purpose
                    });




                }

            }


            if (View.SymptomList != null)
            {
                foreach (var symptom in View.SymptomList)
                {
                    _context.ApptSymptoms?.Add(new ApptSymptom()
                    {
                        Id = Guid.NewGuid(),
                        AppointmentId = appointment.ID,
                        SymptomId = symptom
                    });


                }
            }
            _context?.SaveChanges();

            return RedirectPermanent("~/Manage/QrCode/Generator?id=" + View.ID);
        }

        public IActionResult OnPostEdit()
        {
            if (string.IsNullOrEmpty(View.Symptom))
            {
                ModelState.AddModelError("", "First name cannot be blank.");
                return RedirectPermanent("/manage/admin/dashboard");
            }




            var symptom = _context?.Appointments?.FirstOrDefault(a => a.ID == Guid.Parse(View.SymptomID));

            if (symptom != null)
            {





                _context?.Appointments?.Update(symptom);

                _context?.SaveChanges();

                return RedirectPermanent("~/manage/Patient/dashboard");
            }







            return RedirectPermanent("/manage/Patient/dashboard");
        }

        public IActionResult OnPostCancel()
        {
            if (!Enum.IsDefined(typeof(Status), View.Status))
            {
                ModelState.AddModelError("", ".");
                return RedirectPermanent("/manage/admin/dashboard");
            }




            var status = _context?.Appointments?.FirstOrDefault(a => a.ID == Guid.Parse(View.StatusId));

            if (status != null)
            {

                status.Status = Status.Cancelled;


                _context?.Appointments?.Update(status);

                _context?.SaveChanges();

                return RedirectPermanent("~/manage/Patient/dashboard");
            }







            return RedirectPermanent("/manage/Patient/dashboard");
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



        public class ViewModel : UserViewModel
        {
            public List<Guid>? SymptomId { get; set; }
            public List<Guid>? PurposeList { get; set; }
            public List<Guid>? SymptomList { get; set; }



            public string? CurrentPass { get; set; }
            public string? NewPass { get; set; }
            public string? RetypedPassword { get; set; }

            public string? Symptom { get; set; }
            public Status StatusFilters { get; set; }
            public List<Symptom>? Symptoms { get; set; }
            public List<Infrastructure.Domain.Models.Purpose>? Purposes { get; set; }





            public Guid? ID { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
            public Infrastructure.Domain.Models.Enums.Purpose PurposeOfVisit { get; set; }
            public Guid? PatientID { get; set; }
            public Infrastructure.Domain.Models.Enums.Status Status { get; set; }

            [ForeignKey("PatientID")]
            public Infrastructure.Domain.Models.Patient? Patient { get; set; }




            public Paged<Appointment>? Appointments { get; set; }

            public List<ConsultationRecord>? ConsultationRecords { get; set; }
            public List<Finding>? Findings { get; set; }
            public List<Prescription>? Prescriptions { get; set; }
            public string? SymptomID { get; set; }
            public string? StatusId { get; set; }

            public class IdsValuePair
            {
                public Guid? SId { get; set; }
                public string? SValue { get; set; }
            }


        }
    }
}
