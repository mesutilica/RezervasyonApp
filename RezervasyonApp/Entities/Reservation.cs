using System.ComponentModel.DataAnnotations;

namespace RezervasyonApp.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        [Display(Name = "Kullanıcı")]
        public int UserId { get; set; }
        [Display(Name = "Uzman")]
        public int EmployeeId { get; set; }
        [Display(Name = "Müşteri")]
        public int CustomerId { get; set; }
        [Display(Name = "Randevu Başlangıç Tarihi")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Randevu Bitiş Tarihi")]
        public DateTime EndDate { get; set; }
        public User? User { get; set; }
        public Employee? Employee { get; set; }
        public Customer? Customer { get; set; }
    }
}
