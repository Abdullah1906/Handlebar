namespace HandlebarPractice.Interfaces
{
    public interface ITemplateResolver
    {
        string Resolve(string templateName, string lang, string variant = "default");
    }
}
