namespace RezervasyonApp.Entities
{
    public enum UserType : byte
    {
        None = 0,
        SystemUser = 1, // çalışan personel
        Customer = 2,
        Employee = 3,
        Administrator = 4
    }
}
