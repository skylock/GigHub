using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
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
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly GigsController _sut;

        public GigsControllerTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.SetupGet(u => u.Gigs).Returns(new Mock<IGigRepository>().Object);

            _sut = new GigsController(_mockUnitOfWork.Object);
            _sut.MockCurrentUser("1", "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _sut.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
