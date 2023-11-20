using System.ComponentModel.DataAnnotations;

namespace guran_2_2023.Models
{
    public class Predajcovia
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Meno")]
        public string? Meno { get; set; }
        [Required]
        [Display(Name = "Adresa")]
        public string? Adresa { get; set; }
        //[NotMapped]
        [Display(Name = "Tel")]
        public string? Tel { get; set; }
        //[NotMapped]
        [Display(Name = "Znacky")]
        public string? Znacky { get; set; }
    }
}
