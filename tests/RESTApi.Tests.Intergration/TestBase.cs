using Application.Common.Models;
using Application.Identity.Commands.DTOs;
using Infrastructure.Persistence;
using RESTApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Application.Tests.Intergration
{
    public class TestBase
    {
        protected readonly HttpClient TestClient;

        public TestBase()
        {
            var appFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the database concept
                    var dbService = services.FirstOrDefault(desc => desc.ServiceType == typeof(ApplicationDbContext));
                    if (dbService is not null) services.Remove(dbService);

                    // Add an inmemory db
                    services.AddDbContext<ApplicationDbContext>(options => 
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });
                });

            });
            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var credentials = new LoginRequestDTO("systemAdmin@gmail.com", "WelcomeToPonea123!");

            var response = await TestClient.PostAsJsonAsync("signup", credentials);

            if (response.IsSuccessStatusCode)
                return (await response.Content.ReadFromJsonAsync<ServiceResult<LoginResponseDTO>>()).Data.Token;

            return string.Empty;
        }
    }
}
