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
    
    
}