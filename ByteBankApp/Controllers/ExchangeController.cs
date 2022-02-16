using ByteBank.Service;
using ByteBank.Service.Interfaces;

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

        public string Calculate(string originCurrency, string targetCurrency, decimal amount) 
        {
            string html = View();
            decimal value = _exchangeService.Calculate(originCurrency, targetCurrency, amount);

            html = html.Replace("$MOEDA_ORIGEM", originCurrency).
                Replace("$VALOR_ORIGEM", amount.ToString()).
                Replace("$MOEDA_DESTINO", targetCurrency).
                Replace("$VALOR_DESTINO", value.ToString());

            return html;
        }

        public string Calculate(string targetCurrency, decimal amount) =>
            Calculate("BRL", targetCurrency, amount);   
    }
}
