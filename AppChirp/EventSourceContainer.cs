namespace AppChirp;

class EventSourceContainer<TEventData> : IEventSourceContainer
{
	private readonly EventSource<TEventData> m_EventSource;

	public string Id { get; }

	internal EventSourceContainer(string id)
	{
		Id = id;
		m_EventSource = new EventSource<TEventData>();
	}

	public IEventSource<TData>? GetEventSource<TData>()
		=> typeof(TData) == typeof(TEventData)
			? (IEventSource<TData>)m_EventSource
			: null;
}