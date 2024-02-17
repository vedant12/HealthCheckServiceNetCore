using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using System.Threading.Tasks;
using HealthCheckService.Tasks;
using HealthCheckService.Contract;
using HealthCheckService.Implementation;


public class Program
{
    static async Task Main(string[] args)
    {
        IHost Host = CreateHostBuilder(args).Build();
        await Host.RunAsync();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
        .UseWindowsService()
        .ConfigureServices(services => {
        ConfigureQuartzService(services);
        services.AddScoped<ITaskLogTime, TaskLogTime>();
    });
    private static void ConfigureQuartzService(IServiceCollection services)
    {
        // Add the required Quartz.NET services
        services.AddQuartz(q => {
            // Use a Scoped container to create jobs.
            q.UseMicrosoftDependencyInjectionJobFactory();
            // Create a "key" for the job
            var jobKey = new JobKey("Task1");
            // Register the job with the DI container
            q.AddJob<Task1>(opts => opts.WithIdentity(jobKey));
            // Create a trigger for the job
            q.AddTrigger(opts => opts.ForJob(jobKey) // link to the Task1
                .WithIdentity("Task1-trigger") // give the trigger a unique name
                .WithCronSchedule("0/5 * * * * ?")); // run every 5 seconds
        });
        // Add the Quartz.NET hosted service
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}