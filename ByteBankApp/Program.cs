using ByteBankApp.Infrastructure;

namespace ByteBankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WebApplication webApp = new("http://localhost:5341/");
            webApp.Initialize();
        }
    }
}