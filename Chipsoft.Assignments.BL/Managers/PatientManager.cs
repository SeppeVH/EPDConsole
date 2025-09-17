using System.ComponentModel.DataAnnotations;
using Chipsoft.Assignments.BL.Managers.Interfaces;
using Chipsoft.Assignments.DAL.Repositories.Interfaces;
using Chipsoft.Assignments.Domain;

namespace Chipsoft.Assignments.BL.Managers;

public class PatientManager(IPatientRepository pr): IPatientManager
{
    public IEnumerable<Patient> GetAllPatients()
    {
        return pr.ReadAll();
    }

    public Patient? AddPatient(string name, string email, DateTime birthdate, string address,
        string? phoneNumber = null)
    {
        var newPatient = new Patient(name, email, birthdate, address, phoneNumber);
        Validator.ValidateObject(newPatient, new ValidationContext(newPatient), validateAllProperties: true);
        pr.Create(newPatient);
        return newPatient;
    }
    
    
}