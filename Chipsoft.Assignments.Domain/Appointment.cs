using System.ComponentModel.DataAnnotations;

namespace Chipsoft.Assignments.Domain;

public class Appointment(DateTime appointmentAt, double price, string? description = null) : IValidatableObject
{
    public const int MaxDescriptionLength = 500;
    public const int MinPrice = 5;
    public const int MaxPrice = 100;
    
    [Required]
    public DateTime AppointmentAt { get; init; } = appointmentAt;
    
    [StringLength(MaxDescriptionLength)]
    public string? Description { get; init; } = description;
    
    [Range(MinPrice, MaxPrice)]
    public double Price { get; init; } = price;
    
    [Required] public Patient? Patient { get; set; }
    [Required] public Physician? Physician { get; set; }

    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        if (AppointmentAt < DateTime.Now) results.Add(new ValidationResult("Appointment date cannot be in the past.", [nameof(AppointmentAt)]));
        if (AppointmentAt == default || AppointmentAt.TimeOfDay == TimeSpan.Zero) results.Add(new ValidationResult("Appointment date must include year, month, day, and time.", [nameof(AppointmentAt)]));
        return results;
    }

}