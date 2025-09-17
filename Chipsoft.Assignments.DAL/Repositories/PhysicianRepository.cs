using Chipsoft.Assignments.DAL.EF;
using Chipsoft.Assignments.DAL.Repositories.Interfaces;
using Chipsoft.Assignments.Domain;

namespace Chipsoft.Assignments.DAL.Repositories;

public class PhysicianRepository(EPDDbContext context): Repository<Physician, Guid>(context), IPhysicianRepository
{
    
}