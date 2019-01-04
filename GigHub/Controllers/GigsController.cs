﻿using GigHub.Data;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using GigHub.Repositories;

namespace GigHub.Controllers
{

    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AttendancesRepository _attendancesRepository;
        private readonly GigRepository _gigRepository;

        public GigsController(ApplicationDbContext context)
        {
            _context = context;
            _attendancesRepository = new AttendancesRepository(context);
            _gigRepository = new GigRepository(context);
        }


        [Authorize]
        public IActionResult Mine()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId &&
                            g.DateTime > DateTime.Now &&
                            !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();

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
                Genres = _context.Genres.ToList()
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
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var viewModel = new GigFormViewModel()
            {
                Heading = "Edit a gig",
                Id = gig.Id,
                Genres = _context.Genres.ToList(),
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
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }


            var gig = new Gig
            {
                ArtistId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = _gigRepository.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
                return NotFound();

            if (gig.ArtistId != HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                return Unauthorized();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var gig = _context
                .Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);

            if (gig == null)
                return NotFound();
            
            var model = new GigDetailsViewModel { Gig = gig };
            if (User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.IsAttending = _context.Attendances
                    .Any(a => a.GigId == gig.Id && a.AttendeeId == userId);

                model.IsFollowing = _context.Followings
                    .Any(f => f.FolloweeId == gig.ArtistId && f.FollowerId == userId);
            }

            return View(model);
        }
    }
}