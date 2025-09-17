using System.ComponentModel.DataAnnotations;

namespace Chipsoft.Assignments.Domain;

public abstract class User(string name, string email, string? phoneNumber = null)
{
    public const int MaxNameLength = 255;
    public const int MinNameLength = 2;
    
    [Key]
    public Guid Id { get; set; }

    [StringLength(MaxNameLength, MinimumLength = MinNameLength), Required]
    public string Name { get; init; } = name;

    [EmailAddress, Required]
    public string Email { get; init; } = email;
    
    [Phone]
    public string? PhoneNumber { get; set; } = phoneNumber;
}