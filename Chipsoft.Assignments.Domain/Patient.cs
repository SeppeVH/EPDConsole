namespace Chipsoft.Assignments.Domain;

public class Patient(string name, string email, DateTime birthdate, string address, string? phoneNumber = null) : User(name, email, phoneNumber)
{
    public DateTime Birthdate { get; init; } = birthdate;
    public string Address { get; init; } = address;
}