using System;
using System.Net;
using System.Reflection;
using System.Text;
using ByteBankApp.Utilities;

namespace ByteBankApp.Infrastructure
{
    public class WebApplication
    {
        private readonly string[] _prefixes;
        public WebApplication(params string[] prefixes)
        {
            _prefixes = prefixes ?? throw new ArgumentNullException(nameof(prefixes), 
                "Os prefixos não podem ser nulos");
        }
        
        public void Initialize()
        {
            while (true)
            {
               HandleRequests();
            }
        }

        private void HandleRequests()
        {
            // Objeto responsável por responder as requisições HTTP.
            HttpListener httpListener = new();
            
            // Adicionando os prefixos que o HttpListener deve ouvir.
            foreach (var prefix in _prefixes)
            {
                httpListener.Prefixes.Add(prefix);
            }
            
            // Começa a ouvir a rede.
            httpListener.Start();
            
            // Configurando o contexto.
            // A aplicação trava e fica esperando um requisição acontecer.
            HttpListenerContext context = httpListener.GetContext();
            var request = context.Request;
            var response = context.Response;

            // Para adicionar um conteúdo ao response é necessário converter 
            // o conteúdo em um stream.
            // Obtendo o caminho do recurso dentro do assembly.
            var resourcePath = Util.GetResourceFromAssembly(request.Url.AbsolutePath);
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            
            if (resource == null)
            {
                response.StatusCode = 404;
                response.OutputStream.Close();
            }
            else
            {
                byte[] buffer = new byte[resource.Length];
            
                // Adicionando os dados do stream no buffer.
                resource.Read(buffer, 0, (int) resource.Length);
                response.ContentType = Util.GetContentType(resourcePath);
                response.StatusCode = 200;
                // Define o tamonho da resposta.
                response.ContentLength64 = resource.Length;
                // Escrevendo a resposta no body.
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
            
            httpListener.Stop();
        }
    }
}