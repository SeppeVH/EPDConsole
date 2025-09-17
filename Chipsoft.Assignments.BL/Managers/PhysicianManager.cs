using System.ComponentModel.DataAnnotations;
using Chipsoft.Assignments.BL.Managers.Interfaces;
using Chipsoft.Assignments.DAL.Repositories.Interfaces;
using Chipsoft.Assignments.Domain;

namespace Chipsoft.Assignments.BL.Managers;

public class PhysicianManager(IPhysicianRepository pr): IPhysicianManager
{
    public IEnumerable<Physician> GetAllPhysicians()
    {
        return pr.ReadAll();
    }

    public Physician? AddPhysician(string name, string email, string workFloor, DateTime hiredAt, string? phoneNumber = null)
    {
        var newPhysician = new Physician(name, email, workFloor, hiredAt, phoneNumber);
        Validator.ValidateObject(newPhysician, new ValidationContext(newPhysician), true);
        pr.Create(newPhysician);
        return newPhysician;
    }

    public void DeletePhysician(Guid id)
    {
        pr.Delete(pr.Read(id));
    }
}