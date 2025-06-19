namespace AppChirp;

internal class EventBus : IEventBus
{
	private readonly Dictionary<string, IEventSourceContainer> m_EventSources = [];

	public void RegisterEventSource<TEventData>(string id)
	{
		var source = new EventSourceContainer<TEventData>(id);

		m_EventSources.Add(id, source);
	}

	public IEventPublisher<TEventData>? GetEventPublisher<TEventData>(string id)
		=> GetEventSource<TEventData>(id);

	public IObservable<TEventData>? GetEventObserable<TEventData>(string id)
		=> GetEventSource<TEventData>(id)?.Source;

	private IEventSource<TEventData>? GetEventSource<TEventData>(string id)
		=> m_EventSources.TryGetValue(id, out var source)
			? source.GetEventSource<TEventData>()
			: null;
}
