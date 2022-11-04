public class Benchmarks
{
    private static PostsService PostsService { get; }

    static Benchmarks()
    {
        var host = new HostBuilder()
            .ConfigureServices(services =>
            {
                services.AddHttpClient();
                services.AddTransient<PostsService>();
            })
            .Build();

        PostsService = host.Services.GetRequiredService<PostsService>();
    }

    [Benchmark]
    public async Task GetPosts()
    {
        await PostsService.GetPosts();
    }

    [Benchmark]
    public async Task UpdatePosts()
    {
        await PostsService.ArchivePosts("Blog_000", 2030);
    }

    [Benchmark]
    public async Task InsertPosts()
    {
        await PostsService.InsertPosts();
    }

    [Benchmark]
    public async Task DeletePosts()
    {
        await PostsService.DeletePosts("Blog_000", 2030);
    }

    [Benchmark]
    public async Task GetPost()
    {
        await PostsService.GetPost(1);
    }

    private static int _y = 0;
    [Benchmark]
    public async Task UpdatePost()
    {
        await PostsService.UpdatePost(
            new Post
            {
                Id = ++_y,
                BlogId = 1,
                PublishedOn = DateTime.UtcNow,
                Title = "Updated Post",
                Content = "Yadda Yadda Yadda"
            });
    }

    [Benchmark]
    public async Task InsertPost()
    {
        await PostsService.InsertPost(new Post
        {
            BlogId = 1,
            PublishedOn = DateTime.UtcNow,
            Title = "New Post",
            Content = "Yadda Yadda Yadda"
        });
    }

    private static int _x = 0;
    [Benchmark]
    public async Task DeletePost()
    {
        await PostsService.DeletePost(++_x);
    }
}
