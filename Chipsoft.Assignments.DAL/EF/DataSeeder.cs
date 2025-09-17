using Chipsoft.Assignments.Domain;

namespace Chipsoft.Assignments.DAL.EF;

public static class DataSeeder
{
    public static void SeedData(EPDDbContext context)
    {
        context.Database.EnsureCreated();

        var patients = new List<Patient>
        {
            new Patient("John Doe", "Doe@mail.com", new DateTime(1980, 1, 1), "street 1, 10 ville, city", "1234567890"),
            new Patient("Jane Smith", "Smith@mail.com", new DateTime(1990, 2, 2), "avenue 2, 20 town, city", "0987654321"),
        };
        
        var physicians = new List<Physician>
        {
            new Physician("Alice Johnson", "Johnson@mail.com", "1A", new DateTime(2021, 9, 10)),
            new Physician("Bob Brown", "Brown@mail.com", "2A", new DateTime(2020, 2, 2)),
        };
        
        var appointments = new List<Appointment>
        {
            new Appointment(DateTime.Now.AddDays(1).AddHours(9), 5,"General Checkup") {Patient = patients[0], Physician = physicians[0]},
            new Appointment(DateTime.Now.AddDays(2).AddHours(10), 15, "Broken leg") {Patient = patients[1], Physician = physicians[1]}
        };
        
        context.AddRange(patients);
        context.AddRange(physicians);
        context.AddRange(appointments);
        
        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
}