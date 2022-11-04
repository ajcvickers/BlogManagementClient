using System.Net;

public class PostsServiceTest
{
    private readonly PostsService _postsService;

    public PostsServiceTest()
    {
        _postsService = Server.Host.Services.GetRequiredService<PostsService>();
    }

    [Fact]
    public async Task Can_get_post()
    {
        var post = await _postsService.GetPost(1);

        Assert.Equal("Blog_000_Post_000", post.Title);
    }

    [Fact]
    public async Task Can_update_post()
    {
        var post = await _postsService.GetPost(2);
        Assert.Equal("Blog_000_Post_001", post.Title);

        post.Title = "<Updated>";
        await _postsService.UpdatePost(post);

        var updatedPost = await _postsService.GetPost(2);
        Assert.Equal("<Updated>", updatedPost.Title);
    }

    [Fact]
    public async Task Can_insert_post()
    {
        var post = await _postsService.InsertPost(new Post
        {
            BlogId = 1,
            PublishedOn = DateTime.UtcNow,
            Title = "New Post",
            Content = "Yadda Yadda Yadda"
        });

        Assert.Equal("New Post", post.Title);
        Assert.True(post.Id > 0);

        var insertedPost = await _postsService.GetPost(post.Id);
        Assert.Equal("New Post", insertedPost.Title);
    }

    [Fact]
    public async Task Can_delete_post()
    {
        await _postsService.DeletePost(4);

        try
        {
            await _postsService.GetPost(4);
            Assert.Fail("Expected 404");
        }
        catch (HttpRequestException requestException)
        {
            Assert.Equal(HttpStatusCode.NotFound, requestException.StatusCode);
        }
    }

    [Fact]
    public async Task Can_get_all_posts()
    {
        var posts = (await _postsService.GetPosts()).ToList();

        Assert.True(posts.Count > 0);
        foreach (var post in posts)
        {
            Assert.NotNull(post.Blog);
            Assert.Equal(post.Blog.Id, post.BlogId);
        }
    }

    [Fact]
    public async Task Can_archive_old_posts()
    {
        var postsBefore = (await _postsService.GetPosts()).ToList();
        Assert.Equal(0, postsBefore.Count(p => p.Blog.Name == "Blog_005" && p.Archived));

        await _postsService.ArchivePosts("Blog_005", 2010);

        var postsAfter = (await _postsService.GetPosts()).ToList();
        Assert.Equal(5, postsAfter.Count(p => p.Blog.Name == "Blog_005" && p.Archived));

        foreach (var post in postsAfter)
        {
            if (post.Blog.Name == "Blog_005"
                && post.PublishedOn < new DateTime(2010, 1, 1))
            {
                var publishedYear = post.PublishedOn.Year;
                Assert.True(post.Archived);
                Assert.Equal(post.Banner, $"This post was published in {publishedYear} and has been archived.");
                Assert.EndsWith($" ({publishedYear})", post.Title);
            }
            else if (post.Blog.Name == "Blog_006")
            {
                Assert.False(post.Archived);
                Assert.Null(post.Banner);
            }
        }
    }

    // [Fact]
    // public async Task Can_delete_failed_posts()
    // {
    //     var postsBefore = (await _postsService.GetPosts()).ToList();
    //     Assert.Equal(0, postsBefore.Count(p => p.Blog.Name == "Blog_004" && p.Archived));
    //
    //     await _postsService.ArchivePosts("Blog_004", 2010);
    //     var postsAfterArchive = (await _postsService.GetPosts()).ToList();
    //     Assert.Equal(5, postsAfterArchive.Count(p => p.Blog.Name == "Blog_004" && p.Archived));
    //
    //     await _postsService.DeleteFailedPosts("Blog_004", 3);
    //     var postsAfterDelete = (await _postsService.GetPosts()).ToList();
    //     Assert.Equal(2, postsAfterDelete.Count(p => p.Blog.Name == "Blog_004" && p.Archived));
    // }
}
