using Domain.Common;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// IDomainEventService Interface
    /// </summary>
    public interface IDomainEventService
    {
        /// <summary>
        /// Publish an event
        /// </summary>
        /// <param name="domainEvent">Domain event to be published</param>
        /// <returns>Awaitable Task</returns>
        Task Publish(DomainEvent domainEvent);
    }
}
