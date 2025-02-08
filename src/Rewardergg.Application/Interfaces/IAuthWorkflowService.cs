namespace Rewardergg.Application.Interfaces
{
    public interface IAuthWorkflowService
    {
        Task<string> LoginAsync(string code, CancellationToken cancellationToken);
    }
}
