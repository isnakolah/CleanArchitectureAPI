using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            try
            {
                var userId = _currentUserService.UserId;

                var userName = await _identityService.GetUserNameAsync(userId);

                _logger.LogInformation("EPharmacy Request: {Name} {@UserId} {@UserName} {@Request}",
                    requestName, userId, userName, request);
            }
            catch
            {
                
                if (!requestName.ToLower().Contains("login"))
                        throw new UnauthorizedAccessException();

                _logger.LogWarning("EPharmacy Request: {Name} {@Request} user not logged in",
                    requestName, request);
            }
        }
    }
}
