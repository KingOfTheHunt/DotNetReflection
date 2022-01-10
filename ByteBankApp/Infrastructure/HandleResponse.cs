using System.Net;
using System.Reflection;

namespace ByteBankApp.Infrastructure
{
    public class HandleResponse
    {
        public void Handle(HttpListenerResponse response, string path)
        {
            // Para adicionar um conteúdo ao response é necessário converter 
            // o conteúdo em um stream.
            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);

            if (resourceStream == null)
            {
                response.StatusCode = 404;
                response.OutputStream.Close();
            }
            else
            {
                byte[] buffer = new byte[resourceStream.Length];
                resourceStream.Read(buffer, 0, (int)resourceStream.Length);
                response.StatusCode = 200;
                response.ContentLength64 = resourceStream.Length;
                response.ContentType = Utilities.Util.GetContentType(path);
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
                resourceStream.Close();
            }
        }
    }
}
