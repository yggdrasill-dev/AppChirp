namespace AppChirp;

internal interface IEventSource<TMessage> : IEventPublisher<TMessage>
{
	IObservable<TMessage> Source { get; }
}
