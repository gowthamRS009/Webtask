using System.ComponentModel.DataAnnotations;

namespace Webtask.Models
{
    
        public class Home
        {
            [Key]
            public int Id { get; set; }

            [Required]

            public string Name { get; set; }
            [Required]

            [EmailAddress]

            public string EmailAddress { get; set; }
        }
  }

