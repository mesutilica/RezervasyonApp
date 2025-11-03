using System.ComponentModel.DataAnnotations;

namespace RezervasyonApp.Entities
{
    public class Service
    {
        public int Id { get; set; }
        [Display(Name = "Hizmet Adı"), StringLength(50), Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Name { get; set; }
        [Display(Name = "Hizmet Açıklama"), DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        [Display(Name = "Hizmet Resmi"), StringLength(100)]
        public string? Image { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)] // ScaffoldColumn(false) : sayfa oluştururken bu kolon oluşmasın
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Üst Menüde Göster")]
        public bool IsTopMenu { get; set; }
        [Display(Name = "Anasayfada Göster")]
        public bool IsHome { get; set; }
    }
}
