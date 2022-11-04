public static class Server
{
    public static IHost Host { get; } = new HostBuilder()
        .ConfigureServices(services =>
        {
            services.AddHttpClient();
            services.AddTransient<PostsService>();
        })
        .Build();
}
