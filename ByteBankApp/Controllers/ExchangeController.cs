﻿using ByteBank.Service;
using ByteBank.Service.Interfaces;
using System.IO;
using System.Reflection;

namespace ByteBankApp.Controllers
{
    public class ExchangeController
    {
        private readonly IExchangeService _exchangeService;

        public ExchangeController()
        {
            _exchangeService = new ExchangeService();
        }

        public string GetMXN()
        {
            string html = null;

            decimal value = _exchangeService.Calculate("MXN", "BRL", 1);
            var resource = "ByteBank.App.View.Cambio.MXN.html";
            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);

            // Convertendo o resourceStream em uma cadeia de caracteres.
            using (StreamReader reader = new(resourceStream))
            {
                html = reader.ReadToEnd();
            }

            return html.Replace("$VALOR_MAGICO", value.ToString("c2"));
        }
    }
}
