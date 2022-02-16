using System;
using System.Collections.Generic;
using System.Reflection;

namespace ByteBankApp.Infrastructure.Binding
{
    // Classe resposável por retornar um método de uma controller de uma requisição.
    public class ActionBinder
    {
        public MethodInfo GetMethodInfo(object controller, string path)
        {
            // Verificando se há parâmetros na url.

            if (path.IndexOf('?') < 0)
            {
                var actionName = path.Split(new char[] { '/' },
                    StringSplitOptions.RemoveEmptyEntries)[1];

                MethodInfo methodInfo = controller.GetType().GetMethod(actionName);
                
                return methodInfo;
            }
            else
            {
                var controllerAndActionNames = path.Substring(0, path.IndexOf('?'));
                var actionName = controllerAndActionNames.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[1];
                var queryString = controllerAndActionNames.Substring(path.IndexOf('?') + 1);
                var parameters = GetParameters(queryString);
            }
        }

        /**
         * Este método recebe uma string que contém os parâmetros que foram passados na url
         * e os separam um uma lista de tuplas.
         */
        private IEnumerable<Tuple<string, string>> GetParameters(string queryString)
        {
            var parameters = new List<Tuple<string, string>>();
            var namesAndValues = queryString.Split('&');

            foreach (string item in namesAndValues)
            {
                var parts = item.Split('=');

                parameters.Add(Tuple.Create(parts[0], parts[1]));
            }

            return parameters;
        }
    }
}
