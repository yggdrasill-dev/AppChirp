using AppChirp;
using AppChirp.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddAppChirp(
		this IServiceCollection services,
		Action<AppChirpConfiguration> configure)
	{
		ArgumentNullException.ThrowIfNull(configure, nameof(configure));

		var configuration = new AppChirpConfiguration();
		configure?.Invoke(configuration);

		services.TryAddSingleton<IEventBus>(sp =>
		{
			var eventBus = ActivatorUtilities.CreateInstance<EventBus>(sp);

			foreach (var config in sp.GetServices<AppChirpConfiguration>())
				config.ConfigEventBus(eventBus);

			return eventBus;
		});

		return services
			.AddSingleton(configuration);
	}
}