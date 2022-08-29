using CommonDbLayer.DatabaseEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReaderApi.Services;

namespace ReaderApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly IPaymentService _purchaseBook;
        private readonly IReaderService _readerservice;
        //public MyDigitalBookDbContext dbcontext;

        public ReaderController(IPaymentService purchaseBook, IReaderService readerservice)
        {
            _purchaseBook = purchaseBook;
            _readerservice = readerservice;
        }
        /// <summary>
        /// This method is used to Purchase Book
        /// </summary>
        /// <param name="payment"></param>
        /// <returns>Add new book in purchase</returns>
        [HttpPost]
        public ActionResult PurchaseBook( Payment payment)
        {
            try
            {
                string result = _purchaseBook.BuyBook(payment);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// this method is used to find all books purchased by using paymentID
        /// </summary>
        /// <param name="paymentid"></param>
        /// <returns>returns all purchased books based on purchase it</returns>
        [HttpPost]
        public ActionResult GetPurchasedbookbyId(Payment paymentid)
        {
            try
            {
                IEnumerable<Payment> result = _purchaseBook.GetPurchasedbookbyId(paymentid.PaymentId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return Ok("Please provide correct Paymentid!");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Method is used to get all puchase book list by emailid
        /// </summary>
        /// <param name="emailid"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetAllPurchasedBook(Payment emailid)
        {
            try
            {
                IEnumerable<Payment> result = _purchaseBook.GetPurchasedbookList(emailid.BuyerEmailId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return Ok("You have not purchased any Book!");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// This method gets all books by ID
        /// </summary>
        /// <param name="book"></param>
        /// <returns>returns book on basis of id</returns>

        [HttpPost]
        public ActionResult GetBookById([FromBody] Book book)
        {
            try
            {
                var result = _readerservice.GetBookfromId(book.BookId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest($"Book not Found");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// This Method Returns all book based on categories searched by user
        /// </summary>
        /// <param name="authorName"></param>
        /// <param name="category"></param>
        /// <param name="price"></param>
        /// <returns>returns book lists</returns>
        [HttpGet]
        public IActionResult SearchBookByCategories(string? authorName, string? category, int? price)
        {
            try
            {
                var result = _readerservice.SearchBookByCategory(authorName, category, price);
                return Ok(result.ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
       
        /// <summary>
        /// This method is used to refund and return book
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetRefund(Payment payment)
        {
            
            try
            {
                string res=_readerservice.GetRefund(payment.PaymentId);
                
                if (res != String.Empty)
                {
                    return Ok(res);
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
            
            
        }
    }
}
