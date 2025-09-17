using Chipsoft.Assignments.BL.Managers;
using Chipsoft.Assignments.DAL.EF;
using Chipsoft.Assignments.DAL.Repositories;
using Chipsoft.Assignments.EPDConsole;
using Microsoft.EntityFrameworkCore;

var optionBuilder = new DbContextOptionsBuilder<EPDDbContext>();
optionBuilder.UseSqlite("Data Source=EPD.db");
var context = new EPDDbContext(optionBuilder.Options);
if (context.CreateDataBase(dropDataBase: true))
{
    DataSeeder.SeedData(context);
}

var patientRepository = new PatientRepository(context);
var physicianRepository = new PhysicianRepository(context);
var appointmentRepository = new AppointmentRepository(context);

var patientManager = new PatientManager(patientRepository);
var physicianManager = new PhysicianManager(physicianRepository);
var appointmentManager = new AppointmentManager(appointmentRepository);

var ui = new ConsoleUi(patientManager, physicianManager, appointmentManager, context);
ui.Run();