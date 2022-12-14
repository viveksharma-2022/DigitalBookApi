using System;
using System.Collections.Generic;

namespace CommonDbLayer.DatabaseEntity
{
    public partial class Payment
    {
        public long PaymentId { get; set; }
        public string? BuyerEmailId { get; set; }
        public string? BuyerName { get; set; }
        public long? BookId { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
