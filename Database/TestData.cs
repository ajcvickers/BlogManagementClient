using ModelX;

public class TestData
{
    public static void RecreateDatabase(int blogCount, int postCount)
    {
        using var context = new BlogsContext();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.AddRange(CreateBlogsAndPosts());

        context.SaveChanges();

        IList<Blog> CreateBlogsAndPosts()
        {
            var blogs = new List<Blog>();
            for (var i = 0; i < blogCount; i++)
            {
                var publishedDate = DateTime.UtcNow;

                var blog = new Blog()
                {
                    Name = $"Blog_{i:000}",
                    Account = new Account
                    {
                        Owner = "The Magic Unicorns",
                        Details = new AccountDetails
                        {
                            Credits = 10,
                            //IsPremium = i < 3
                        },
                    }
                };

                for (var j = 0; j < postCount; j++)
                {
                    blog.Posts.Add(new Post
                    {
                        Title = $"{blog.Name}_Post_{j:000}",
                        Content = "Yadda Yadda Yadda",
                        PublishedOn = publishedDate,
                    });

                    publishedDate = publishedDate.AddMonths(postCount > 10 ? -3 : -36);
                }

                blogs.Add(blog);
            }

            return blogs;
        }
    }
}
