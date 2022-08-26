using CommonDbLayer.DatabaseEntity;

namespace AuthorApi.Services
{
    public interface IBookService
    {
        string AddBooks(Book book);
        List<Book> GetAllBooks();
        string UpdateBooks(Book book);
        List<Book> GetPurchasedbookbyId(long id);
        string BlockaBook(int id);
        string UnBlockaBook(int id);
    }
}
