using System.ComponentModel.DataAnnotations;

namespace Chipsoft.Assignments.Domain;

public class Physician(string name, string email, string workFloor, DateTime hiredAt, string? phoneNumber = null) 
    : User(name, email, phoneNumber), IValidatableObject
{
    // Example work floor format: "1A"
    [Required, RegularExpression(@"^\d{1}[A-Z]{1}$", ErrorMessage = "Work floor must be in the format of a digit followed by an uppercase letter (e.g., '1A').")]
    public string WorkFloor { get; init; } = workFloor;
    
    [Required] 
    public DateTime HiredAt { get; init; } = hiredAt;

    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        if (HiredAt > DateTime.Now) results.Add(new ValidationResult("Hired date cannot be in the future.", [nameof(HiredAt)]));
        return results;
    }
}