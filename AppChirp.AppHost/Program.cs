var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AppChirpDemo>("AppChirpDemo")
	.WithUrls(ctx =>
	{
		foreach (var url in ctx.Urls)
		{
			if (string.IsNullOrEmpty(url.DisplayText))
			{
				url.DisplayText = $"Swagger ({url.Endpoint?.Scheme?.ToUpper()})";
				url.Url = "/swagger/index.html";
			}
		}
	});

builder.Build().Run();
