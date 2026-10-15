using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    [Table("CATEGORIES")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryID { get; set; }

        [Required, MaxLength(30)]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        public int OrganiserID { get; set; }

        [Required]
        public int EventID { get; set; }
    }
}
