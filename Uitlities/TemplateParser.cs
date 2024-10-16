using HandlebarsDotNet;

public static class TemplateParser
{
    public static string ParseEmailTemplate(string template, object model)
    {
        var compiledTemplate = Handlebars.Compile(template);
        string emailContent = compiledTemplate(model);
        return emailContent;
    }
}
