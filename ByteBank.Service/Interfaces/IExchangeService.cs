namespace ByteBank.Service.Interfaces
{
    public interface IExchangeService
    {
        decimal Calculate(string originCurrency, string targetCurrency, decimal amount);
    }
}
