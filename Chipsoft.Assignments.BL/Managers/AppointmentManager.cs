using System.ComponentModel.DataAnnotations;
using Chipsoft.Assignments.BL.Managers.Interfaces;
using Chipsoft.Assignments.DAL.Repositories.Interfaces;
using Chipsoft.Assignments.Domain;

namespace Chipsoft.Assignments.BL.Managers;

public class AppointmentManager(IAppointmentRepository ap) : IAppointmentManager
{
    public IEnumerable<Appointment> GetAllAppointments()
    {
        return ap.ReadAll();
    }

    public Appointment? AddAppointment(DateTime appointmentAt, double price, string? description, Patient patient, Physician physician)
    {
        var existingAppointments = ap.GetAppointmentsByPhysicianAndDate(physician.Id, appointmentAt);
        if (existingAppointments.Any()) throw new ValidationException("The physician already has an appointment at this date and time.");
        
        var newAppointment = new Appointment(appointmentAt, price, description) {Patient = patient, Physician = physician};
        Validator.ValidateObject(newAppointment, new ValidationContext(newAppointment), validateAllProperties: true);
        ap.Create(newAppointment);
        return newAppointment;
    }

    public void DeleteAppointment(Guid id)
    {
        ap.Delete(ap.Read(id));
    }
}