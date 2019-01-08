using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GigHub.Tests.Extensions
{
    public static class ApiControllerExtension
    {
        public static void MockCurrentUser(this ControllerBase controller, string userId, string userName)
        {
            var identity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, userId)
            }));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = identity }
            };
        }
    }
}
