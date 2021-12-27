using System;
using System.Net;
using System.Text;

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
            var content = "Olá, mundo!";
            var contentStream = Encoding.UTF8.GetBytes(content);
            
            response.ContentType = "text/html; charset=utf-8";
            response.StatusCode = 200;
            // Define o tamonho da resposta.
            response.ContentLength64 = contentStream.Length;
            // Escrevendo a resposta no body.
            response.OutputStream.Write(contentStream, 0, contentStream.Length);
            response.OutputStream.Close();
            
            httpListener.Stop();
        }
    }
}