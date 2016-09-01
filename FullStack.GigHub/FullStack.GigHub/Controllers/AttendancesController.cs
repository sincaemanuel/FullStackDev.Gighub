using FullStack.GigHub.Dtos;
using FullStack.GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace FullStack.GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {

        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userid = User.Identity.GetUserId();
            if (_context.Attendances.Any(x => x.AttendeeId == userid && x.GigId == dto.GigId))
            {
                return BadRequest("This attendance already exists.");
            }
            var attendance = new Attendance()
            {
                GigId = dto.GigId,
                AttendeeId = userid
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }
    }
}
