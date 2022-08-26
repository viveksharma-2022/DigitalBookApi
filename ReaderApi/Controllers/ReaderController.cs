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
        //private readonly 

        public ReaderController(IPaymentService purchaseBook, IReaderService readerservice)
        {
            _purchaseBook = purchaseBook;
            _readerservice = readerservice;
        }
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
        [HttpPost]
        public ActionResult GetPurchasedbookbyId([FromBody] long paymentid)
        {
            try
            {
                var result = _purchaseBook.GetPurchasedbookbyId(paymentid);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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

        [HttpGet]
        public IActionResult SearchBookByCategories(string? authorName, string? category, int price)
        {

            var result = _readerservice.SearchBookByCategory(authorName, category, price);
            return Ok(result.ToList());

        }


    }
}
