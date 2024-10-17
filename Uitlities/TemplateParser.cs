
using System.Dynamic;
using HandlebarsDotNet;
using Newtonsoft.Json;

public static class TemplateParser
{
    public static string ParseEmailTemplate(string template, object model)
    {
        var compiledTemplate = Handlebars.Compile(template);
        var data = JsonConvert.DeserializeObject<dynamic>(model.ToString());
        string emailContent = compiledTemplate(data);
        return emailContent;
    }
}
