namespace LungMed.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string FullNameWithId => $"Id: {Id} - {FirstName} {LastName}";
    }
}
