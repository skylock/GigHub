using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly GigsController _sut;
        protected internal string _userId;

        public GigsControllerTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.SetupGet(u => u.Gigs).Returns(new Mock<IGigRepository>().Object);

            _sut = new GigsController(_mockUnitOfWork.Object);
            _userId = "1";
            _sut.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _sut.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockUnitOfWork.Setup(r => r.Gigs.GetGigWithAttendees(1)).Returns(gig);

            var result = _sut.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig { ArtistId = _userId + "-" };

            _mockUnitOfWork.Setup(r => r.Gigs.GetGigWithAttendees(1)).Returns(gig);

            var result = _sut.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();
        }
    }
}
