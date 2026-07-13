using System.ComponentModel.DataAnnotations;

namespace SMS.Models
{
    public class BaseEntity
    {
        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

    public class Product1 : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

