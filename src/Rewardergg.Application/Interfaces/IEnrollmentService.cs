public interface IEnrollmentService
{
    Task<(bool Success, string Message)> EnrollUserInEventAsync(Guid userId, Guid eventId, CancellationToken cancellationToken);
}