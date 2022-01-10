using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ByteBankApp.Controllers
{
    public abstract class ControllerBase
    {
        // CallerMemberName retorna o nome do método que chamou este método.
        protected string View([CallerMemberName] string actionName = null)
        {
            string content = null;

            // Pega o nome da classe filha.
            string controller = GetType().Name.Replace("Controller", "");

            string resourcePath = $"ByteBankApp.View.{controller}.{actionName}.html";
            Stream resourceStream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(resourcePath);

            using (StreamReader reader = new(resourceStream))
            {
                content = reader.ReadToEnd();
            }

            return content;
        }
    }
}
