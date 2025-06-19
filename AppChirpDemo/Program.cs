using AppChirp;
using AppChirpDemo.Models;

var builder = WebApplication.CreateBuilder(args);
{
	builder.AddServiceDefaults();

	builder.Services
		.AddProblemDetails()
		.AddOpenApi("v1")
		.AddEndpointsApiExplorer();

	builder.Services
		.AddAppChirp(config => config
			.RegisterEventSource<string>("demo"))
		.AddKeyedSingleton(
			"demo",
			(sp, _) => sp.GetRequiredService<IEventBus>()
				.GetEventPublisher<string>("demo")!)
		.AddHostedService<MessageBackgroudService>();
}

var app = builder.Build();
{
	app.MapPut(
		"/demo",
		async (
			[FromKeyedServices("demo")] IEventPublisher<string> eventPublisher,
			CancellationToken cancellationToken) => await eventPublisher.PublishAsync(
				$"Hello, {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
				cancellationToken).ConfigureAwait(false)
				? Results.Accepted()
				: Results.Conflict("Failed to publish event."))
		.WithOpenApi();

	app.MapOpenApi();
	app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
	app.MapDefaultEndpoints();
}

app.Run();
