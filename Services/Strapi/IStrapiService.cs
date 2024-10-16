public interface IStrapiService
{
    Task<List<TemplateDto>> GetEmailTemplatesAsync();
    Task<TemplateDto> GetEmailTemplateByNameAsync(string name);
}