using RinhaCrebito.Entities.Common;

namespace RinhaCrebito.Entities
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public int Limit { get; set; }
        public Balance Balance { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = [];

        public Customer() { }

        public static Customer Create(string name, int limit)
        {
            return new()
            {
                Name = name,
                Limit = limit
            };
        }
    }
}