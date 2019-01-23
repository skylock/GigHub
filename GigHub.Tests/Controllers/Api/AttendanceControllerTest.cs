using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendanceControllerTest
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private AttendancesController _sut;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.SetupGet(u => u.Attendances).Returns(new Mock<IAttendancesRepository>().Object);

            _sut = new AttendancesController(_mockUnitOfWork.Object);
            _userId = "1";
            _sut.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Attend_GigNotAttended_ShouldReturnOk()
        {
            var result = _sut.Attend(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<OkResult>();
        }
    }
}
