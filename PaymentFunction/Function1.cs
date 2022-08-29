using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;

namespace PaymentFunction
{
    public static class Function1
    {
        [FunctionName("CreatePayment")]
        public static async Task<IActionResult> CreatePayment(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "purchasebook")] HttpRequest req,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<PaymentModel>(requestBody);
            try
            {
                using (SqlConnection connection = new SqlConnection("Server=tcp:digitalbookserver.database.windows.net,1433;Initial Catalog=MyDigitalBookdb;Persist Security Info=False;User ID=vivek;Password=pass@word1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    connection.Open();
                    if (!String.IsNullOrEmpty(input.BuyerEmailId) && !string.IsNullOrEmpty(input.BuyerName))
                    {

                        //var dt= input.PaymentDate.ToString("yyyy-MM-dd HH:mm:ss");
                        var query = $"INSERT INTO [Payment] (BuyerEmailId, BuyerName, BookId, Title,Price,PaymentDate) VALUES('{input.BuyerEmailId}', '{input.BuyerName}' , '{input.BookId}', '{input.Title}', '{input.Price}', '{input.PaymentDate}')";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
            return new OkResult();
        }
    }
}
