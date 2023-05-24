using Capstonep2.Infrastructure.Domain.Models;
using Capstonep2.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Capstonep2.Pages.Manage.Admin
{
    public class AdminEditApptModel : PageModel
    {
        private ILogger<System.Index> _logger;
        private DefaultDBContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public AdminEditApptModel(DefaultDBContext context, ILogger<System.Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }
        public void OnGet()
        {
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
        public class ViewModel : Appointment
        {
            public List<Appointment>? Appointments { get; set; }
            public List<Symptom>? Symptoms { get; set; }
            public List<Purpose>? Purposes { get; set; }



        }
    }
}
