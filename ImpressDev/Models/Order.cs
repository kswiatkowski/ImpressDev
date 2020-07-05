using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImpressDev.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Required(ErrorMessage = "Wprowadź imię.")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwisko.")]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Wprowadź ulicę.")]
        [StringLength(50)]
        public string Street { get; set; }
        [Required(ErrorMessage = "Wprowadź miasto.")]
        [StringLength(50)]
        public string City { get; set; }
        [Required(ErrorMessage = "Wprowadź kod pocztowy.")]
        [StringLength(50)]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Wprowadź numer telefonu.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Wprowadź adres email.")]
        [EmailAddress(ErrorMessage = "Błędny format adresu email.")]
        public string Email { get; set; }
        public string Comment { get; set; }
        public DateTime DateAdded { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal Price { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
    
    public enum OrderStatus
    {
        Nowe,
        Zrealizowane,
        Anulowane
    }
}