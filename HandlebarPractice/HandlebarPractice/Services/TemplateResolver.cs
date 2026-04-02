using HandlebarPractice.Interfaces;

namespace HandlebarPractice.Services
{
    public class TemplateResolver :ITemplateResolver
    {
        public string Resolve(string templateName, string lang, string variant = "default")
        {
            if (variant == "modern")
                return $"Templates/{lang}/{templateName}-modern.hbs";

            return $"Templates/{lang}/{templateName}.hbs";
        }
    }
}
