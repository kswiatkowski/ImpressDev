using System.ComponentModel.DataAnnotations;

namespace ImpressDev.Models
{
    public class UserData
    {
        [Display(Name = "Imię")]
        public string Name { get; set; }
        
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        
        [Display(Name = "Ulica")]
        public string Street { get; set; }
        
        [Display(Name = "Miasto")]
        public string City { get; set; }
        
        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }
       
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Błędny format adresu email")]
        public string Email { get; set; }
    }
}