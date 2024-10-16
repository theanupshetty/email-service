
public class StrapiService : IStrapiService
{
    private readonly string _strapiUrl;
    private readonly string _bearerToken;
    private readonly HttpClient _httpClient;

    public StrapiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _strapiUrl = configuration["Strapi:Url"];
        _bearerToken = configuration["Strapi:Bearer"];
    }

    private void AddBearerTokenToHeaders()
    {
        if (!string.IsNullOrEmpty(_bearerToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _bearerToken);
        }
    }

    public async Task<List<TemplateDto>> GetEmailTemplatesAsync()
    {
        AddBearerTokenToHeaders();
        var response = await _httpClient.GetAsync($"{_strapiUrl}/api/templates");
        response.EnsureSuccessStatusCode();
        var templates = await response.Content.ReadFromJsonAsync<StrapiResponse<TemplateDto>>();
        return templates.Data;
    }

    public async Task<TemplateDto> GetEmailTemplateByNameAsync(string name)
    {
        AddBearerTokenToHeaders();
        var response = await _httpClient.GetAsync($"{_strapiUrl}/api/email-templates?filters[TemplateName][$eq]={name}");
        response.EnsureSuccessStatusCode();
        var templates = await response.Content.ReadFromJsonAsync<StrapiResponse<TemplateDto>>();
        return templates.Data.FirstOrDefault();
    }
}