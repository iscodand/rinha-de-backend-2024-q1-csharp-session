using System.ComponentModel.DataAnnotations.Schema;
using RinhaCrebito.Entities.Common;

namespace RinhaCrebito.Entities
{
    public class Transaction : Entity
    {
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public int Value { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public Transaction()
        {
            CreatedAt = DateTime.Now;
        }

        public static Transaction Create(int customerId, int value, string type, string description)
        {
            return new()
            {
                CustomerId = customerId,
                Value = value,
                Type = type,
                Description = description
            };
        }
    }
}