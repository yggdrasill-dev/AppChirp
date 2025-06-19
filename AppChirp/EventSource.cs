using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AppChirp;

internal class EventSource<TMessage> : IEventSource<TMessage>
{
	private readonly Subject<EventData<TMessage>> m_Subject = new();

	public IObservable<TMessage> Source => m_Subject
		.SelectMany(data =>
		{
			var activity = AppChripUtility.ActivitySource.StartActivity(
				"Processing internal message",
				ActivityKind.Internal,
				data.ParentContext ?? default);

			return activity is null
				? Observable.Return(data.Message)
				: Observable.Using(
					() => activity,
					_ => Observable.Return(data.Message));
		});

	public Task<bool> PublishAsync(TMessage eventData, CancellationToken cancellationToken = default)
	{
		m_Subject.OnNext(new EventData<TMessage>(Activity.Current?.Context, eventData));

		return Task.FromResult(true);
	}
}