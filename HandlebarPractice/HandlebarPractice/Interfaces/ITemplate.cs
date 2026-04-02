namespace HandlebarPractice.Interfaces
{
    public interface ITemplate
    {
        string Render(string templatePath, object model);
    }
}
