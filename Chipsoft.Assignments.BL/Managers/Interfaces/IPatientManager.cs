using Chipsoft.Assignments.Domain;

namespace Chipsoft.Assignments.BL.Managers.Interfaces;

public interface IPatientManager
{
    public IEnumerable<Patient> GetAllPatients();
    public Patient? AddPatient(string name, string email, DateTime birthdate, string address,
        string? phoneNumber = null);
}