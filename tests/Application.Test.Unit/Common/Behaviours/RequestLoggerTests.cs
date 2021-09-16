using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Provider.Commands.CreatePharmacy;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tests.Unit.Common.Behaviours
{
    public class RequestLoggerTests
    {
        private readonly Mock<ILogger<CreatePharmacyCommand>> _logger;
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly Mock<IIdentityService> _identityService;


        public RequestLoggerTests()
        {
            _logger = new Mock<ILogger<CreatePharmacyCommand>>();

            _currentUserService = new Mock<ICurrentUserService>();

            _identityService = new Mock<IIdentityService>();
        }

        [Test]
        public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
        {
            _currentUserService.Setup(x => x.UserId).Returns("Administrator");

            var requestLogger = new LoggingBehaviour<CreatePharmacyCommand>(
                _logger.Object, _currentUserService.Object, _identityService.Object);

            await requestLogger.Process(new CreatePharmacyCommand(
                new Provider { Name = "Test", Location = "TestLocation" }, "route"), new CancellationToken());

            _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
        {
            var requestLogger = new LoggingBehaviour<CreatePharmacyCommand>(
                _logger.Object, _currentUserService.Object, _identityService.Object);

            await requestLogger.Process(new CreatePharmacyCommand(
                new Provider { Name = "Test2", Location ="TestLocation2"}, "route"), new CancellationToken());

            _identityService.Verify(i => i.GetUserNameAsync(null), Times.Never);
        }
    }
}
