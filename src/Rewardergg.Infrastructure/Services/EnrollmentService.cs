using Microsoft.EntityFrameworkCore;
using Rewardergg.Domain;
using Rewardergg.Infrastructure.Persitence;

public class EnrollmentService : IEnrollmentService
{
    private readonly AppDbContext _dbContext;

    public EnrollmentService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(bool Success, string Message)> EnrollUserInEventAsync(Guid userId, Guid eventId, CancellationToken cancellationToken)
    {
        //// Check if already enrolled
        //var exists = await _dbContext.UserEnrollments
        //    .AnyAsync(e => e.UserId == userId && e.EventId == eventId, cancellationToken);

        //if (exists)
        //    return (false, "User already enrolled in this event.");

        //// Add enrollment
        //var enrollment = new UserEnrollment
        //{
        //    UserId = userId,
        //    EventId = eventId,
        //    EnrollmentDate = DateTime.UtcNow
        //};

        //await _dbContext.UserEnrollments.AddAsync(enrollment, cancellationToken);
        //await _dbContext.SaveChangesAsync(cancellationToken);

        return (true, "Enrollment successful.");
    }
}