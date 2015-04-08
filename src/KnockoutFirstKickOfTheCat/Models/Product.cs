using System;
using System.ComponentModel.DataAnnotations;

namespace KnockoutFirstKickOfTheCat.Models
{
    public class Product
    {
        public Product()
        {
            IsActive = true;
        }

        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage="Name must be 20 characters or less")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage="{0} must be greater than zero")]
        public decimal UnitPrice { get; set; }

        [Range(typeof(DateTime), "1980/1/1", "2050/12/31",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime ActiveDate { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // derived fields
        public string ActiveDateString
        {
            get { return ActiveDate.ToString("yyyy/MM/dd"); }
            set { ActiveDate = DateTime.Parse(value); }
        }

        // meta-data fields
        public bool IsDirty { get; set; }
        public bool IsActive { get; set; }

    }

}