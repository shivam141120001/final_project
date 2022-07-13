using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerMicroservice.Models
{
    public class Manager
    {
        [Key]
        public int ManagerId { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^([6-9]{1}[0-9]{9})$", ErrorMessage = "Invalid Mobile Number.")]
        public long ContactNumber { get; set; }

        [Required]
        public string Locality { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
