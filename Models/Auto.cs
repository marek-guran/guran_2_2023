using System.ComponentModel.DataAnnotations;

namespace guran_2_2023.Models
{
        public class Auto
        {
            [Key]
            public int ID { get; set; }
            [Required]
            [Display(Name = "Znacka")]
            public string? Znacka { get; set; }
            [Required]
            [Display(Name = "Cena")]
            public string? Cena { get; set; }
            //[NotMapped]
            [Display(Name = "Fotografia")]
            public string? Fotografia { get; set; }
        }
}
