using ByteBankApp.Infrastructure;

namespace ByteBankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("A aplicação está sendo ouvida na " +
                "url: http://localhost:5341/");

            WebApplication webApp = new("http://localhost:5341/");
            webApp.Initialize();
        }
    }
}