using System.Diagnostics;

namespace AppChirp;

record EventData<TMessage>(
	ActivityContext? ParentContext,
	TMessage Message);