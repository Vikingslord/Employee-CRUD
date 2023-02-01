using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{

    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string Age { get; set; }
        public int IsActive { get; set; }

    }
}
