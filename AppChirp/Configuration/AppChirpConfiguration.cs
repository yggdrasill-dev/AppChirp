namespace AppChirp.Configuration;

public class AppChirpConfiguration
{
	private readonly List<Action<EventBus>> m_RegisterActions = [];

	public AppChirpConfiguration RegisterEventSource<TEventData>(string id)
	{
		m_RegisterActions.Add(eventBus => eventBus.RegisterEventSource<TEventData>(id));

		return this;
	}

	internal void ConfigEventBus(EventBus eventBus)
	{
		foreach (var action in m_RegisterActions)
			action(eventBus);
	}
}