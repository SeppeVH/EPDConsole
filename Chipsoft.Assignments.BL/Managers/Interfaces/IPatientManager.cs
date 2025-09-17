using Chipsoft.Assignments.Domain;

namespace Chipsoft.Assignments.BL.Managers.Interfaces;

public interface IPatientManager
{
    IEnumerable<Patient> GetAllPatients();
    Patient? AddPatient(string name, string email, DateTime birthdate, string address,
        string? phoneNumber = null);
    void DeletePatient(Guid id);
}