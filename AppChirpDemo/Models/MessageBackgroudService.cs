using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using AppChirp;

namespace AppChirpDemo.Models;

public class MessageBackgroudService(
	IEventBus eventBus,
	ILogger<MessageBackgroudService> logger) : BackgroundService
{
	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var demoSource = eventBus.GetEventObserable<string>("demo");

		return demoSource is not null
			? demoSource
				.Do(message => logger.LogInformation("Received message: {Message}", message))
				.ToTask(stoppingToken)
			: Task.CompletedTask;
	}
}