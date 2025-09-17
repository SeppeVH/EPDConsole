using System.ComponentModel.DataAnnotations;

namespace Chipsoft.Assignments.Domain;

public class Patient(string name, string email, DateTime birthdate, string address, string? phoneNumber = null) : User(name, email, phoneNumber), IValidatableObject
{
    [Required] public DateTime Birthdate { get; init; } = birthdate;
    
    // address regular expression format (street 39, 1234 AB, City)
    [Required, RegularExpression(@"^[\w\s]+ \d{1,4}, \d{1,4} [\w\s]+, [\w\s]+$", ErrorMessage = "Address must be in the correct format.")]
    public string Address { get; init; } = address;
    
    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        if (Birthdate >= DateTime.Now) results.Add(new ValidationResult("Birthdate must be in the past.", new[] { nameof(Birthdate) }));
        return results;
    }

    public override string ToString()
    {
        return $"Patient: {Name}, Email: {Email}, Birthdate: {Birthdate.ToShortDateString()}, Address: {Address}, PhoneNumber: {PhoneNumber}";
    }
}