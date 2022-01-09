using System;
using System.Reflection;

namespace ByteBankApp.Utilities
{
    public static class Util
    {
        public static string GetResourceFromAssembly(string path)
        {
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            return assemblyName + path.Replace("/", ".");
        }

        public static string GetContentType(string path)
        {
            string extension = GetExtension(path);

            return extension switch
            {
                "html" => "text/html; charset=utf-8",
                "css" => "text/css; charset=utf-8",
                "js" => "application/js; charset=utf-8",
                _ => throw new ArgumentException("Não há um implementação para este tipo de arquivo", nameof(extension))
            };
        }

        private static string GetExtension(string file)
        {
            return file[(file.LastIndexOf(".") + 1)..file.Length];
        }
    }
}