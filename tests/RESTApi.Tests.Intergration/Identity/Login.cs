using Application.Common.Models;
using Application.Identity.Commands.DTOs;
using Application.Tests.Intergration;
using Helpers;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace RESTApi.Tests.Intergration.Identity
{
    public class LoginTests : TestBase
    {
        [Fact]
        public async Task POST_logs_in_user()
        {
            var credentials = new LoginRequestDTO("systemAdmin@ponea.com", "Systemdmin@123!");

            var response = await TestClient.PostAsJsonAsync(V1ApiRoutes.System.Identity.Login, credentials);

            var responseData = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task POST_creates_a_user()
        {
            var newUser = new CreateUserDTO("Test User", "TestUser", "Male", "0700000000", "testUser@test.com");

            var response = await TestClient.PostAsJsonAsync(V1ApiRoutes.System.Users.Create, newUser);

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }

}
