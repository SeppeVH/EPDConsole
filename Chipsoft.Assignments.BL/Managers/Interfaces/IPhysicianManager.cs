using Chipsoft.Assignments.Domain;

namespace Chipsoft.Assignments.BL.Managers.Interfaces;

public interface IPhysicianManager
{
    IEnumerable<Physician> GetAllPhysicians();
    Physician? AddPhysician(string name, string email, string workFloor, DateTime hiredAt, string? phoneNumber = null);
    void DeletePhysician(Guid id);
}