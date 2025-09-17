using Chipsoft.Assignments.DAL.EF;
using Chipsoft.Assignments.DAL.Repositories.Interfaces;
using Chipsoft.Assignments.Domain;

namespace Chipsoft.Assignments.DAL.Repositories;

public class PatientRepository(EPDDbContext context) : Repository<Patient, Guid>(context), IPatientRepository
{
    
}