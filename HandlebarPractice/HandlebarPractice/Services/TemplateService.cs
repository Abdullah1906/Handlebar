using HandlebarPractice.Interfaces;
using HandlebarsDotNet;

namespace HandlebarPractice.Services
{
    public class TemplateService:ITemplate
    {
        /// <summary>
        /// Render a Handlebars template file with a given model
        /// </summary>
        /// <param name="templatePath">Relative path to .hbs file</param>
        /// <param name="model">The model to inject</param>
        /// <returns>Rendered HTML string</returns>
        public string Render(string templatePath, object model)
        {
            if (!File.Exists(templatePath))
                throw new FileNotFoundException($"Template file not found: {templatePath}");

            var templateText = File.ReadAllText(templatePath);

            var template = Handlebars.Compile(templateText);

            var html = template(model);

            return html;
        }
    }
}
