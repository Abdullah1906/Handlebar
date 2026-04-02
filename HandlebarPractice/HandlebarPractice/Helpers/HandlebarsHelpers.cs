using HandlebarsDotNet;

namespace HandlebarPractice.Helpers
{
    public static class HandlebarsHelpers
    {
        public static void RegisterHelpers()
        {
            Handlebars.RegisterHelper("formatDate", (writer, context, parameters) =>
            {
                if (parameters.Length == 0 || parameters[0] == null)
                {
                    writer.WriteSafeString("");
                    return;
                }

                var date = Convert.ToDateTime(parameters[0]);
                writer.WriteSafeString(date.ToString("dd MMM yyyy hh:mm tt"));
            });
        }
    }
}
