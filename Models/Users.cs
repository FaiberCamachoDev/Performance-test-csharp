using System.ComponentModel.DataAnnotations;

namespace Performance_test_csharp.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage =  "Username is required")]
    [StringLength(100, ErrorMessage = "The name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    [StringLength(10, ErrorMessage = "The document cannot exceed 10 characters.")]
    public string Document { get; set; } = string.Empty; // único
    
    [Required(ErrorMessage = "Phone is required")]
    [StringLength(10, ErrorMessage = "The number phone cannot exceed 10 numbers.")]
    public string Phone { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    [StringLength(100, ErrorMessage = "The email cannot exceed 100 characters.")]
    public string Email { get; set; } = string.Empty;      // único

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}