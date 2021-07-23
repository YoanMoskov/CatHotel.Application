namespace CatHotel.Data.Models
{
    using System.Collections.Generic;

    public class Grooming
    {
        public string Id { get; set; }

        public string DescribePreferredStyle { get; set; }

        public IEnumerable<Cat> Cats { get; set; } = new List<Cat>();

        public string PaymentId { get; set; }

        public Payment Payment { get; set; }
    }
}