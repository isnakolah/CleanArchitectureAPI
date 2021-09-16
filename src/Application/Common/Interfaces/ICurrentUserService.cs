namespace Application.Common.Interfaces
{
    /// <summary>
    /// ICurrentUserService interface
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Get the ID of the current user
        /// </summary>
        string UserId { get; }
    }
}
