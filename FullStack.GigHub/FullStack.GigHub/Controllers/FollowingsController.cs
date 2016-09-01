using FullStack.GigHub.Dtos;
using FullStack.GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace FullStack.GigHub.Controllers
{
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userid = User.Identity.GetUserId();
            if (_context.Followings.Any(x => x.FolloweeId == userid && x.FolloweeId == dto.Followeeid))
            {
                return BadRequest("This following already exists.");
            }
            var following = new Following
            {
                FollowerId = userid,
                FolloweeId = dto.Followeeid
            };
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }
    }
}