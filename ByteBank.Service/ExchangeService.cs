using ByteBank.Service.Interfaces;
using System;

namespace ByteBank.Service
{
    public class ExchangeService : IExchangeService
    {
        private readonly Random _random = new Random();

        public decimal Calculate(string originCurrency, string targetCurrency, decimal amount)
        {
            return (decimal) _random.NextDouble() * amount;
        }
    }
}
