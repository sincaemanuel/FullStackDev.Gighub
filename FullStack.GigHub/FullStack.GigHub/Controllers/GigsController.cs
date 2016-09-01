using FullStack.GigHub.Models;
using FullStack.GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FullStack.GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Create()
        {
            GigFormViewModel model = new GigFormViewModel()
            {

                Genres = _context.Genres
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _context.Genres.ToList();
                return View("Create", model);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = model.GetDateTime(),
                GenreId = model.Genre,
                Venue = model.Venue
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Attending()
        {
            var userid = User.Identity.GetUserId();
            var gigs = _context.Attendances.Where(x => x.AttendeeId == userid).Select(x => x.Gig).Include(x => x.Artist).Include(x => x.Genre).ToList();

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm attending"
            };
            return View("Gigs", viewModel);
        }

        public ActionResult Following()
        {
            var userid = User.Identity.GetUserId();
            var followees = _context.Followings.Where(x => x.FollowerId == userid).Select(x => x.Followee).ToList();
            return View(followees);
        }

    }
}