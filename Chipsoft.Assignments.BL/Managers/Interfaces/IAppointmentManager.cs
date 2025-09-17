using Chipsoft.Assignments.Domain;

namespace Chipsoft.Assignments.BL.Managers.Interfaces;

public interface IAppointmentManager
{
    IEnumerable<Appointment> GetAllAppointments();
    Appointment? AddAppointment(DateTime appointmentAt, double price, string? description, Patient patient,
        Physician physician);
    void DeleteAppointment(Guid id);
}