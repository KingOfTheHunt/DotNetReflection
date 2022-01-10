using System;
using System.Net;
using System.Reflection;
using System.Text;
using ByteBankApp.Controllers;
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


            if (Util.IsAFile(request.Url.AbsolutePath))
            {
                // Obtendo o caminho do recurso dentro do assembly.
                var resourcePath = Util.GetResourceFromAssembly(request.Url.AbsolutePath);
                HandleResponse handleResponse = new HandleResponse();
                handleResponse.Handle(response, resourcePath);
            }
            else if (request.Url.AbsolutePath == "/Exchange/MXN")
            {
                ExchangeController exchangeController = new ExchangeController();
                var content = exchangeController.GetMXN();

                byte[] buffer = Encoding.UTF8.GetBytes(content);

                response.StatusCode = 200;
                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/html; charset=utf-8";
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
            else if (request.Url.AbsolutePath == "/Cambio/USD")
            {
                ExchangeController exchangeController = new ExchangeController();
                var content = exchangeController.GetUSD();

                byte[] buffer = Encoding.UTF8.GetBytes(content);

                response.StatusCode = 200;
                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/html; charset=utf-8";
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }

            httpListener.Stop();
        }
    }
}