using Chipsoft.Assignments.DAL.EF;
using Chipsoft.Assignments.DAL.Repositories.Interfaces;
using Chipsoft.Assignments.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chipsoft.Assignments.DAL.Repositories;

public class AppointmentRepository(EPDDbContext context) : Repository<Appointment, Guid>(context), IAppointmentRepository 
{
    public IEnumerable<Appointment> ReadAppointmentsByPhysicianAndDate(Guid physicianId, DateTime date)
    {
        return context.Appointments
            .Include(a => a.Physician)
            .Where(a => a.Physician != null && a.Physician.Id == physicianId && a.AppointmentAt.Date == date.Date)
            .ToList();
    }

    public IEnumerable<Appointment> ReadAppointmentsWithPatientAndPhysician()
    {
        return context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Physician)
            .ToList();
    }
}