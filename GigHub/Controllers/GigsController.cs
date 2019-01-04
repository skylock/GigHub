using GigHub.Data;
using GigHub.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace GigHub.Controllers
{

    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AttendancesRepository _attendancesRepository;
        private readonly GigRepository _gigRepository;
        private readonly GenreRepository _genreRepository;
        private readonly FollowingRepository _followingRepository;
        private readonly UnitOfWork _unitOfWork;

        public GigsController(ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(context);
            _followingRepository = new FollowingRepository(context);
            _genreRepository = new GenreRepository(context);
            _attendancesRepository = new AttendancesRepository(context);
            _gigRepository = new GigRepository(context);
        }


        [Authorize]
        public IActionResult Mine()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var gigs = _gigRepository.GetUpcomingGigsByArtist(userId);

            return View(gigs);
        }

        [Authorize]
        public IActionResult Attending()
        {
            var viewModel = new GigsViewModel
            {
                UpcomingGigs = _gigRepository.GetGigsUserAttending(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _attendancesRepository.GetFutureAttendances(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)).ToLookup(g => g.GigId)
            };

            return View("Gigs", viewModel);
        }

        // Get: Gigs
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Heading = "Add a Gig",
                Genres = _genreRepository.GetGenres()
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }


        [Authorize]
        public IActionResult Edit(int id)
        {
            var gig = _gigRepository.GetGig(id);

            if (gig == null)
                return NotFound();

            if (gig.ArtistId != HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                return Unauthorized();

            var viewModel = new GigFormViewModel()
            {
                Heading = "Edit a gig",
                Id = gig.Id,
                Genres = _genreRepository.GetGenres(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _genreRepository.GetGenres();
                return View("GigForm", viewModel);
            }


            var gig = new Gig
            {
                ArtistId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _gigRepository.Add(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _genreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _gigRepository.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
                return NotFound();

            if (gig.ArtistId != HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                return Unauthorized();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var gig = _gigRepository.GetGig(id);

            if (gig == null)
                return NotFound();

            var model = new GigDetailsViewModel { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                model.IsAttending = _attendancesRepository.GetAttendance(gig.Id, userId) != null;

                model.IsFollowing = _followingRepository.GetFollowings(userId, gig.ArtistId) != null;
            }

            return View(model);
        }
    }
}