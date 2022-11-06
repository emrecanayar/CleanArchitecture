namespace rentACar.Infrastructure.Adapters.FakePOSService
{
    public interface IPOSService
    {
        Task Pay(string invoiceNo, decimal price);
    }
}
