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
            else
            {
                HandleResponseController responseController = new();
                responseController.Handle(response, request.Url.AbsolutePath);
            }

            httpListener.Stop();
        }
    }
}