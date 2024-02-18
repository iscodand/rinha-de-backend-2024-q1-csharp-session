using System.ComponentModel.DataAnnotations;

namespace RinhaCrebito.Entities.Common
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}