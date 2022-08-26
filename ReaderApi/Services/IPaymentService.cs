using CommonDbLayer.DatabaseEntity;

namespace ReaderApi.Services
{
    public interface IPaymentService
    {
        string BuyBook(Payment payment);
        List<Payment> GetPurchasedbookList(string emailid);
        List<Payment> GetPurchasedbookbyId(long paymentid);
    }
}