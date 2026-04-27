using System.ComponentModel.DataAnnotations;

namespace Performance_test_csharp.Models;

public class DeportiveSpace
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage =  "Name space is required")]
    [StringLength(100, ErrorMessage = "The name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Type of space is required")]
    [StringLength(50, ErrorMessage = "The Deport type cannot exceed 50 characters.")]
    public string DeportSpaceType { get; set; } = string.Empty; // único
    
    [Required(ErrorMessage = "Capacity is required.")]
    [Range(1, 100, ErrorMessage = "Capacity must be greather than 0.")]
    public int Capacidad { get; set; }

    //navegation property
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    
}