using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Capstonep2.Infrastructure.Domain;
using Capstonep2.Infrastructure.Domain.Models;
using Capstonep2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Capstonep2.Pages.Manage.Consultation
{
    [Authorize(Roles = "admin")]
    public class ViewDetailsModel : PageModel
    {
        private ILogger<System.Index> _logger;
        private DefaultDBContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public ViewDetailsModel(DefaultDBContext context, ILogger<System.Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public void AppointmentModel(Guid? id = null)
        {
            var appointment = _context?.Appointments?.Where(a => a.ID == id).Include(a => a.Patient).FirstOrDefault();


            var apptSymptoms = _context?.ApptSymptoms?.Where(a => a.AppointmentId == id).ToList(); //Get ApptSymptomList
            var apptPurposes = _context?.ApptPurposes?.Where(a => a.AppointmetId == id).ToList();
            var apptFindings = _context?.ApptFindings?.Where(a => a.AppointmentId == id).ToList();
            var apptPrescriptions = _context?.ApptPrecriptions?.Where(a => a.AppointmentId == id).ToList();

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

                foreach(var item in apptPurposes)
                {
                    purpose = _context?.Purposes?.Where(a => a.Id == item.PurposeId).FirstOrDefault();
                    if (purpose != null)
                    {
                        purposeList.Add(purpose.Name);
                    }

                }

                foreach(var item in apptFindings)
                {
                    finding = _context?.Findings?.Where(a => a.ID == item.FindingId).FirstOrDefault();
                    if (finding != null)
                    {
                        findingList.Add(finding.FName);
                    }
                }

                foreach(var item in apptPrescriptions)
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
        }


        public void OnGet(Guid? id = null, Guid? crid = null)

        {
            Guid? apptID = id;
            AppointmentModel(apptID);
            View.ApptId = apptID;
        }
        public void OnPostAddConsul()
        {
            
            

            
            var appt = _context?.Appointments?.Where(a => a.ID == View.ApptId).FirstOrDefault();

  
                appt.FDescription = View.FDesc;
                appt.PDescription = View.PDesc;
               appt.Status = Infrastructure.Domain.Models.Enums.Status.Completed;
                _context?.Appointments?.Update(appt);




            if (View.PList != null)
            {
                foreach (var prescription in View.PList)
                {


                    _context?.ApptPrecriptions?.Add(new ApptPrecription()
                    {
                        Id = Guid.NewGuid(),
                        AppointmentId = View.ApptId,
                        PrescriptionId = prescription

                    });




                }

            }

            if (View.FList != null)
            {
                foreach (var finding in View.FList)
                {
                    _context.ApptFindings.Add(new ApptFinding()
                    {
                        Id = Guid.NewGuid(),
                        AppointmentId = View.ApptId,
                        FindingId = finding
                    });

                }
            }



            
            _context?.SaveChanges();
            AppointmentModel(View.ApptId);
        }
        public void OnPostEditstatus()
        {
            
            var appointment = _context?.Appointments?.Where(a => a.ID == View.ApptId).FirstOrDefault();
            if (appointment != null)
            {
                appointment.Status = View.StatusEdit;
                _context?.Appointments.Update(appointment);
            }
            AppointmentModel(View.ApptId);
            _context.SaveChanges();

        }
        public void OnPostUpdate()
        {

            var appointment = _context?.Appointments?.Where(a => a.ID == View.ApptId).FirstOrDefault();
            if (appointment != null)
            {

                appointment.StartTime = View.StartTime;
                appointment.EndTime = View.StartTime.AddHours(1);

                var apptSymptoms = _context?.ApptSymptoms.Where(a => a.AppointmentId == View.ApptId).ToList();
                var apptPurposes = _context?.ApptPurposes.Where(a => a.AppointmetId == View.ApptId).ToList();

                _context?.Appointments.Update(appointment);

                if (apptSymptoms != null && apptPurposes != null)
                {
                    _context?.ApptSymptoms.RemoveRange(apptSymptoms);
                    _context?.ApptPurposes.RemoveRange(apptPurposes);
                }
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


                if (View.SymptomsEdit != null)
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
                
            }
            AppointmentModel(View.ApptId);
        }

        [HttpGet("dahilan")]
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

        [HttpGet("sintomas")]
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
        [HttpGet("gamot")]
        public JsonResult? OnGetGamot(int pageIndex = 1, string? keyword = "", int pageSize = 10)
        {
            return new JsonResult(_context.Prescriptions!.Select(a => new LookupDto.Result()
            {
                Id = a.ID.ToString(),
                Text = a.GName ?? ""


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

        [HttpGet("dahilanedit")]
        public JsonResult? OnGetDahilanEdit(int pageIndex = 1, string? keyword = "", int pageSize = 10)
        {
            return new JsonResult(_context.Purposes!.Select(a => new LookupDto.Result()
            {
                Id = a.Id.ToString(),
                Text = a.Name ?? ""


            })
            .AsQueryable()
            .GetLookupPaged(pageIndex, pageSize));
        }
        [HttpGet("sintomasedit")]
        public JsonResult? OnGetSintomasEdit(int pageIndex = 1, string? keyword = "", int pageSize = 10)
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

        }
    }

}
