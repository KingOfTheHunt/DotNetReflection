using ByteBank.Service;
using ByteBank.Service.Interfaces;
using System.IO;
using System.Reflection;

namespace ByteBankApp.Controllers
{
    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;

        public ExchangeController()
        {
            _exchangeService = new ExchangeService();
        }

        public string MXN()
        {
            string html = View();
            decimal value = _exchangeService.Calculate("MXN", "BRL", 1);
            
            return html.Replace("$VALOR_MAGICO", value.ToString("c2"));
        }

        public string USD()
        {
            string html = View();
            decimal value = _exchangeService.Calculate("USD", "BRL", 1);
            
            return html.Replace("$VALOR_MAGICO", value.ToString("c2"));
        }
    }
}
