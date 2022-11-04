public class PostsService
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    private readonly HttpClient _httpClient;

    public PostsService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<Post> GetPost(int id)
    {
        await using var contentStream = await (await _httpClient.GetAsync($"http://localhost:5200/api/posts/{id}"))
            .EnsureSuccessStatusCode().Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<Post>(contentStream, _serializerOptions);
    }

    public async Task<IEnumerable<Post>> GetPosts()
    {
        await using var contentStream = await (await _httpClient.GetAsync($"http://localhost:5200/api/posts"))
            .EnsureSuccessStatusCode().Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<IEnumerable<Post>>(contentStream, _serializerOptions);
    }

    public async Task<Post> InsertPost(Post post)
    {
        await using var contentStream = await (await _httpClient.PostAsJsonAsync($"http://localhost:5200/api/posts", post))
            .EnsureSuccessStatusCode().Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<Post>(contentStream, _serializerOptions);
    }

    public async Task<Post> UpdatePost(Post post)
    {
        await using var contentStream = await (await _httpClient.PutAsJsonAsync($"http://localhost:5200/api/posts", post))
            .EnsureSuccessStatusCode().Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<Post>(contentStream, _serializerOptions);
    }

    public async Task DeletePost(int id)
        => (await _httpClient.DeleteAsync($"http://localhost:5200/api/posts/{id}")).EnsureSuccessStatusCode();

    public async Task ArchivePosts(string blogName, int priorToYear)
        => (await _httpClient.SendAsync(new HttpRequestMessage(
                HttpMethod.Put,
                $"http://localhost:5200/api/posts/archive?blogName={UrlEncoder.Default.Encode(blogName)}&priorToYear={priorToYear}")))
            .EnsureSuccessStatusCode();

    public async Task DeletePosts(string blogName, int priorToYear)
        => (await _httpClient.SendAsync(new HttpRequestMessage(
                HttpMethod.Delete,
                $"http://localhost:5200/api/posts/delete?blogName={UrlEncoder.Default.Encode(blogName)}&priorToYear={priorToYear}")))
            .EnsureSuccessStatusCode();

    public async Task InsertPosts()
        => (await _httpClient.SendAsync(new HttpRequestMessage(
                HttpMethod.Post,
                $"http://localhost:5200/api/posts/insert")))
            .EnsureSuccessStatusCode();
}
