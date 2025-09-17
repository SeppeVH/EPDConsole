using Chipsoft.Assignments.Domain;

namespace Chipsoft.Assignments.DAL.Repositories.Interfaces;

public interface IAppointmentRepository: IRepository<Appointment, Guid>
{
    IEnumerable<Appointment> GetAppointmentsByPhysicianAndDate(Guid physicianId, DateTime date);
}