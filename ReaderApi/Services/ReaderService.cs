using CommonDbLayer.DatabaseEntity;

namespace ReaderApi.Services
{
    public class ReaderService : IReaderService
    {
        private readonly MyDigitalBookDbContext _dbContext;
        public ReaderService(MyDigitalBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Book GetBookfromId(long id)
        {
            try
            {

                return _dbContext.Books.FirstOrDefault(x => x.BookId == id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Book> SearchBookByCategory(string? authorName, string? category, int? price)
        {
            IEnumerable<Book>? books = null;
            var request = _dbContext.Books.Where(b => b.Active == true);

            if (!string.IsNullOrWhiteSpace(authorName))
            {
                books = request.Where(x => x.AuthorName == authorName).ToList();
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                books=request.Where(x => x.Category == category).ToList();

            }
            if(price!=null && price != 0)
            {
                books=books.Where(x => x.Price == price).ToList();
            }
            if (books == null)
            {
                books = request;
            }
            return books.ToList();
        }
    }
}
