using System.ComponentModel.DataAnnotations;

namespace RezervasyonApp.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Display(Name = "Adı"), StringLength(50)]
        public string? Name { get; set; } // ? işareti bu alanın nullable yani boş geçilebilir olmasını sağlar
        [Display(Name = "Soyadı"), StringLength(50)]
        public string? Surname { get; set; }
        [StringLength(50), EmailAddress, Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Email { get; set; }
        [Display(Name = "Şifre"), StringLength(50), Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Password { get; set; }
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [ScaffoldColumn(false)]
        public Guid? UserGuid { get; set; } = Guid.NewGuid();
    }
}
