
using System.ComponentModel.DataAnnotations;
using Chipsoft.Assignments.BL.Managers.Interfaces;
using Chipsoft.Assignments.DAL.EF;
using Chipsoft.Assignments.Domain;

namespace Chipsoft.Assignments.EPDConsole;

public class ConsoleUi(IPatientManager patientManager, IPhysicianManager physicianManager, IAppointmentManager appointmentManager, EPDDbContext context)
{
    private void AddPatient()
    {
        var validInput = false;

        while (!validInput)
        {
            Console.WriteLine("Add Patient:");
            try
            {
                var name = ConsoleInput("Enter name: ");
                var email = ConsoleInput("Enter email: ");
                var phoneNumber = ConsoleInput("Enter phone number (optional): ");
                if (phoneNumber == "") phoneNumber = null;

                var birthDate = DateTime.Parse(ConsoleInput("Enter birthdate and time (yyyy-MM-dd): "));
                var address = ConsoleInput("Enter address (street 1, 1000 town, city): ");
                
                var newPatient = patientManager.AddPatient(name, email, birthDate, address, phoneNumber) ?? throw new ValidationException("Could not create patient.");
                Console.WriteLine("New patient added:");
                ColorMessage(newPatient.ToString(), ConsoleColor.Green);
                validInput = true;
            }
            catch (ValidationException e)
            {
                ColorMessage(e.Message, ConsoleColor.Red);
                ColorMessage("Please try again.", ConsoleColor.Red);
                validInput = false;
            }
            catch (FormatException e)
            {
                ColorMessage("Invalid date format. Please try again.", ConsoleColor.Red);
                validInput = false;
            }
        }
    }

    private void ShowAppointment()
    {
        Console.WriteLine("Appointments:");
        var appointments = appointmentManager.GetAllAppointments().ToList();
        if (appointments.Count == 0)
        {
            ColorMessage("No appointments found.", ConsoleColor.Yellow);
            return;
        }
        foreach (var appointment in appointments)
        {
            Console.WriteLine(appointment);
        }
    }

    private void AddAppointment()
    {
        var validInput = false;

        while (!validInput)
        {
            Console.WriteLine("Add Appointment:");
            try
            {
                var appointmentAt = DateTime.Parse(ConsoleInput("Enter date and time (yyyy-MM-dd HH:mm): "));
                var price = double.Parse(ConsoleInput("Enter price: "));
                var description = ConsoleInput("Enter description: ");
                if (description == "") description = null;
                
                var patients = patientManager.GetAllPatients().ToList();
                var physicians = physicianManager.GetAllPhysicians().ToList();
                var selectedPatient = SelectFromList(patients, "patient") as Patient ?? throw new ValidationException("Selected user is not a patient.");
                var selectedPhysician = SelectFromList(physicians, "physician") as Physician ?? throw new ValidationException("Selected user is not a physician.");
                
                var newAppointment = appointmentManager.AddAppointment(appointmentAt, price, description, selectedPatient, selectedPhysician) ?? throw new ValidationException("Could not create appointment.");
                Console.WriteLine("New appointment added:");
                ColorMessage(newAppointment.ToString(), ConsoleColor.Green);
                validInput = true;
            }
            catch (ValidationException e)
            {
                ColorMessage(e.Message, ConsoleColor.Red);
                ColorMessage("Please try again.", ConsoleColor.Red);
                validInput = false;
            }
            catch (FormatException e)
            {
                ColorMessage("Invalid date or price format. Please try again.", ConsoleColor.Red);
                validInput = false;
            }
        }
    }

    private void DeletePhysician()
    {
        Console.WriteLine("Delete Physician:");
        var physicians = physicianManager.GetAllPhysicians().ToList();
        if (physicians.Count == 0)
        {
            ColorMessage("No physicians available to delete.", ConsoleColor.Yellow);
            return;
        }
        try
        {
            var selectedPhysician = SelectFromList(physicians, "physician");
            physicianManager.DeletePhysician(selectedPhysician.Id);
            ColorMessage($"Physician {selectedPhysician.Name} deleted successfully.", ConsoleColor.Green);
        }
        catch (Exception e)
        {
            ColorMessage("Deletion failed, Please try again.", ConsoleColor.Red);
        }
    }

    private void AddPhysician()
    {
        var validInput = false;

        while (!validInput)
        {
            Console.WriteLine("Add Physician:");
            try
            {
                var name = ConsoleInput("Enter name: ");
                var email = ConsoleInput("Enter email: ");
                var phoneNumber = ConsoleInput("Enter phone number (optional): ");
                if (phoneNumber == "") phoneNumber = null;

                var workFloor = ConsoleInput("Enter work floor (e.g., 1A): ");
                var hireAt = DateTime.Parse(ConsoleInput("Enter hire date (yyyy-MM-dd): "));
                
                
                var newPhysician = physicianManager.AddPhysician(name, email, workFloor, hireAt, phoneNumber) ?? throw new ValidationException("Could not create physician.");
                Console.WriteLine("New physician added:");
                ColorMessage(newPhysician.ToString(), ConsoleColor.Green);
                validInput = true;
            }
            catch (ValidationException e)
            {
                ColorMessage(e.Message, ConsoleColor.Red);
                ColorMessage("Please try again.", ConsoleColor.Red);
                validInput = false;
            }
            catch (FormatException e)
            {
                ColorMessage("Invalid date format. Please try again.", ConsoleColor.Red);
                validInput = false;
            }
        }
    }

    private void DeletePatient()
    {
        Console.WriteLine("Delete Patient:");
        var patients = patientManager.GetAllPatients().ToList();
        if (patients.Count == 0)
        {
            ColorMessage("No patients available to delete.", ConsoleColor.Yellow);
            return;
        }
        try
        {
            var selectedPatient = SelectFromList(patients, "patient");
            patientManager.DeletePatient(selectedPatient.Id);
            ColorMessage($"Patient {selectedPatient.Name} deleted successfully.", ConsoleColor.Green);
        }
        catch (Exception e)
        {
            ColorMessage("Deletion failed, Please try again.", ConsoleColor.Red);
        }
    }
    
    public void Run()
    {
        while (ShowMenu())
        {
            //Continue
        }
    }
    
    private bool ShowMenu()
    {
        /*
        Console.Clear();
        foreach (var line in File.ReadAllLines("logo.txt"))
        {
            Console.WriteLine(line);
        }
        */
        Console.WriteLine("");
        Console.WriteLine("1 - Patient toevoegen");
        Console.WriteLine("2 - Patienten verwijderen");
        Console.WriteLine("3 - Arts toevoegen");
        Console.WriteLine("4 - Arts verwijderen");
        Console.WriteLine("5 - Afspraak toevoegen");
        Console.WriteLine("6 - Afspraken inzien");
        Console.WriteLine("7 - Sluiten");
        Console.WriteLine("8 - Reset db");

        if (int.TryParse(Console.ReadLine(), out int option))
        {
            switch (option)
            {
                case 1:
                    AddPatient();
                    return true;
                case 2:
                    DeletePatient();
                    return true;
                case 3:
                    AddPhysician();
                    return true;
                case 4:
                    DeletePhysician();
                    return true;
                case 5:
                    AddAppointment();
                    return true;
                case 6:
                    ShowAppointment();
                    return true;
                case 7:
                    return false;
                case 8:
                    context.CreateDataBase(true);
                    return true;
                default:
                    return true;
            }
        }
        return true;
    }
    
    private static string ConsoleInput(string prompt)
    {
        Console.Write(prompt);
        Console.ForegroundColor = ConsoleColor.Blue;
        var result = Console.ReadLine() ?? "";
        Console.ForegroundColor = ConsoleColor.Gray;
        return result;
    }
    
    private static void ColorMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    
    private static User SelectFromList(IEnumerable<User> items, string itemName)
    {
        Console.WriteLine($"Select {itemName}:");
        var itemList = items.ToList();
        for (var i = 0; i < itemList.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {itemList[i].Name}");
        }
        var selectedIndex = int.Parse(ConsoleInput($"Select {itemName} by number: ")) - 1;
        if (selectedIndex < 0 || selectedIndex >= itemList.Count)
        {
            throw new ValidationException($"Invalid {itemName} selection.");
        }
        
        var selectedItem = itemList[selectedIndex];
        return selectedItem;
    }
}