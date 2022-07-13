using System.ComponentModel.DataAnnotations;

namespace PropertyMicroservice.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        [Required]
        public string PropertyType { get; set; }

        [Required]
        public string Locality { get; set; }

        [Required]
        public int Budget { get; set; }

    }
}
