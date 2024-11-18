using WebAPIDBM.Domain.Entities;
using System.Text.Json;

public class ProdutoService : IProdutoService
{
    private readonly HttpClient _httpClient;

    public ProdutoService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("WebAPIDBMClient");
    }

    public async Task<IEnumerable<Produto>> GetProdutosAsync()
    {
        var response = await _httpClient.GetAsync("produtos");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<Produto>>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<Produto> GetProdutoByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"produtos/{id}");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Produto>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<bool> CreateProdutoAsync(Produto produto)
    {
        var response = await _httpClient.PostAsJsonAsync("produtos", produto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateProdutoAsync(Produto produto)
    {
        var response = await _httpClient.PutAsJsonAsync($"produtos/{produto.Id}", produto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProdutoAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"produtos/{id}");
        return response.IsSuccessStatusCode;
    }
}
