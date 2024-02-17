namespace HealthCheckService.Contract
{
    public interface ITaskLogTime
    {
        Task DoWork(CancellationToken cancellationToken);
        Task Execute();
    }
}
