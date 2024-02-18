using System.ComponentModel.DataAnnotations.Schema;
using RinhaCrebito.Entities.Common;

namespace RinhaCrebito.Entities
{
    public class Balance : Entity
    {
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public int Total { get; set; }

        public Balance() { }

        public static Balance Create(int customerId, int total = 0)
        {
            return new()
            {
                CustomerId = customerId,
                Total = total
            };
        }
    }
}