using CommonDbLayer.DatabaseEntity;

namespace ReaderApi.Services
{
    public interface IReaderService
    {
        Book GetBookfromId(long id);
        IEnumerable<Book> SearchBookByCategory(string? authorName, string? category, int? price);
        string GetRefund(long paymentId);
    }
}