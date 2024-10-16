public class EmailDto
{
    public string To { get; set; }
    public string[] CC { get; set; }
    public string[] BCC { get; set; }
    public string TemplateName { get; set; }
    public object TemplateData { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public bool IsBodyHtml { get; set; } = true;
}

public class TemplateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
}

public class StrapiResponse<T>
{
    public List<T> Data { get; set; }
}