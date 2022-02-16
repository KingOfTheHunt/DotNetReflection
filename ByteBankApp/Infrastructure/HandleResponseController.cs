using ByteBankApp.Infrastructure.Binding;
using System;
using System.Net;
using System.Text;

namespace ByteBankApp.Infrastructure
{
    public class HandleResponseController
    {
        private string _controllerFullName = "ByteBankApp.Controllers";
        private readonly ActionBinder _actionBinder = new ActionBinder();

        // Este método vai receber um caminho que vai ser composto pelo controller/action.
        // Cria um instancia de um objeto dinâmicamente a partir do nome do controller.
        // E executa o método de mesmo nome da action.
        public void Handle(HttpListenerResponse response, string path)
        {
            string[] splitedPath = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string controllerName = splitedPath[0];
            string action = splitedPath[1];

            _controllerFullName += $".{controllerName}Controller";

            // Activator é uma classe responsável por criar instancias.
            var objectHandle = Activator.CreateInstance("ByteBankApp", _controllerFullName);
            // Desempacotando o objeto.
            var controller = objectHandle.Unwrap();
            // Acessando as informações do método.
            var methodInfo = _actionBinder.GetMethodInfo(controller, path);
            // Executa o método.
            var resultAction = (string) methodInfo.Invoke(controller, null);

            byte[] buffer = Encoding.UTF8.GetBytes(resultAction);

            response.StatusCode = 200;
            response.ContentLength64 = buffer.Length;
            response.ContentType = "text/html; charset=utf-8";
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }
    }
}
