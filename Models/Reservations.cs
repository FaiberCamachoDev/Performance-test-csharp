using System.ComponentModel.DataAnnotations;

namespace Performance_test_csharp.Models;

public class Reservation
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "User is required")]
    public int UserId { get; set; }
    public User? User { get; set; }
    
    [Required(ErrorMessage = "The Sport-Space is required")]
    public int SpaceId { get; set; }
    public DeportiveSpace? SportSpace { get; set; }
    
    [Required(ErrorMessage = "The date is required")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    
    [Required(ErrorMessage = "The Start date is required")]
    [DataType(DataType.Time)]
    public TimeSpan StartTime { get; set; }
    
    [Required(ErrorMessage = "The End date is required")]
    [DataType(DataType.Time)]
    public TimeSpan FinishTime { get; set; }
    
    [Required(ErrorMessage = "The status is required.")]
    [StringLength(20)]
    public string Status { get; set; } = "Active";
}