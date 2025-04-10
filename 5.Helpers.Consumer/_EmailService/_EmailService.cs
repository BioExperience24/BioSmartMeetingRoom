
namespace _5.Helpers.Consumer._EmailService
{
    public sealed class _EmailService
    {
        public static string LoadTemplate(string typeOfMail)
        {
            try
            {   
                var emailTemplate = _EmailServiceDict.EmailTemplate[typeOfMail];
                
                var rootPath = Directory.GetCurrentDirectory(); // Mengambil root dari project yang sedang berjalan
                // var templatePath = Path.GetFullPath(Path.Combine(rootPath, "../5.Helpers.Consumer", "EmailTemplate", emailTemplate));
                var templatePath = Path.GetFullPath(Path.Combine(rootPath, "wwwroot", "emailtemplates", emailTemplate));

                if (File.Exists(templatePath))
                {
                    return File.ReadAllText(templatePath);
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new FormatException($"An error occurred while loading the template: {ex.Message}");
            }
        }
    }
}