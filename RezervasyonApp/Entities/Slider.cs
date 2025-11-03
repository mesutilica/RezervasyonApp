using System.ComponentModel.DataAnnotations;

namespace RezervasyonApp.Entities
{
    public class Slider
    {
        public int Id { get; set; }
        [Display(Name = "Başlık"), StringLength(250)]
        public string? Title { get; set; }
        [Display(Name = "Açıklama"), DataType(DataType.MultilineText), StringLength(500)]
        public string? Description { get; set; }
        [Display(Name = "Resim"), StringLength(100)]
        public string? Image { get; set; }
    }
}
