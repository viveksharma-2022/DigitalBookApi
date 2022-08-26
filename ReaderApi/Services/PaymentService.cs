using CommonDbLayer.DatabaseEntity;

namespace ReaderApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly MyDigitalBookDbContext _dbContext;
        public PaymentService(MyDigitalBookDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public List<Payment> GetPurchasedbookList(string emailid)
        {
            try
            {
                return _dbContext.Payments.Where(x => x.BuyerEmailId == emailid).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<Payment> GetPurchasedbookbyId(long paymentid)
        {
            try
            {
                return _dbContext.Payments.Where(x => x.PaymentId == paymentid).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public string BuyBook(Payment payment)
        {
            if (payment.BuyerName != null && payment.BuyerEmailId != null)
            {
                _dbContext.Payments.Add(payment);
                _dbContext.SaveChanges();
                return $"Book Bought successfully";               

            }
            else
            {
                return "EmailId is incorrect";
            }
        }


    }
}
