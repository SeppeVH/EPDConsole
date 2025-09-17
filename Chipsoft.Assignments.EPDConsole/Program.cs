using Chipsoft.Assignments.BL.Managers;
using Chipsoft.Assignments.DAL.EF;
using Chipsoft.Assignments.DAL.Repositories;
using Chipsoft.Assignments.EPDConsole;
using Microsoft.EntityFrameworkCore;

var optionBuilder = new DbContextOptionsBuilder<EPDDbContext>();
optionBuilder.UseSqlite("Data Source=EPD.db");
var context = new EPDDbContext(optionBuilder.Options);
context.CreateDataBase(dropDataBase: true);

var patientRepository = new PatientRepository(context);

var patientManager = new PatientManager(patientRepository);

var ui = new ConsoleUi(patientManager, context);
ui.Run();