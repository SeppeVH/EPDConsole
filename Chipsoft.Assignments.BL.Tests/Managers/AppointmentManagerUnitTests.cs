using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chipsoft.Assignments.BL.Managers;
using Chipsoft.Assignments.DAL.Repositories.Interfaces;
using Chipsoft.Assignments.Domain;
using Moq;
using NUnit.Framework;

namespace Chipsoft.Assignments.BL.Tests.Managers;

[TestFixture]
[TestOf(typeof(AppointmentManager))]
public class AppointmentManagerUnitTests
{
    private AppointmentManager _appointmentManager;
    private Mock<IAppointmentRepository> _mockAppointmentRepository;

    private Patient _patient;
    private Physician _physician;
    
    [SetUp]
    public void SetUp()
    {
        _mockAppointmentRepository = new Mock<IAppointmentRepository>();
        _appointmentManager = new AppointmentManager(_mockAppointmentRepository.Object);
        
        _patient = new Patient("test", "test@mail.com", DateTime.Now.AddYears(-20), "Main St 29, 10 ville, City");
        _physician = new Physician("John Smith", "john@mail.com", "1A", DateTime.Now.AddYears(-10));
    }
    
    [Test]
    public void AddAppointment_ThrowsValidationException_WhenAppointmentAtInPast()
    {
        // Arrange
        _mockAppointmentRepository.Setup(ar => ar.GetAppointmentsByPhysicianAndDate(It.IsAny<Guid>(), It.IsAny<DateTime>()))
            .Returns(new List<Appointment>());
        
        var invalidAppointmentAt = DateTime.Now.AddDays(-1);
        var validPrice = Appointment.MinPrice + 1.0;
        
        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => 
            _appointmentManager.AddAppointment(invalidAppointmentAt, validPrice, null, _patient, _physician));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("Appointment date cannot be in the past"));
        _mockAppointmentRepository.Verify(pr => pr.Create(It.IsAny<Appointment>()), Times.Never);
    }
    
    [Test]
    public void AddAppointment_ThrowsValidationException_WhenDescriptionTooLong()
    {
        // Arrange
        _mockAppointmentRepository.Setup(ar => ar.GetAppointmentsByPhysicianAndDate(It.IsAny<Guid>(), It.IsAny<DateTime>()))
            .Returns(new List<Appointment>());
        
        var validAppointmentAt = DateTime.Now.AddDays(1);
        var validPrice = Appointment.MinPrice + 1.0;
        var invalidDescription = new string('a', Appointment.MaxDescriptionLength + 1);
        
        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => 
            _appointmentManager.AddAppointment(validAppointmentAt, validPrice, invalidDescription, _patient, _physician));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("maximum length"));
        _mockAppointmentRepository.Verify(pr => pr.Create(It.IsAny<Appointment>()), Times.Never);
    }
    
    [Test]
    public void AddAppointment_ThrowsValidationException_WhenPriceNotInRange()
    {
        // Arrange
        _mockAppointmentRepository.Setup(ar => ar.GetAppointmentsByPhysicianAndDate(It.IsAny<Guid>(), It.IsAny<DateTime>()))
            .Returns(new List<Appointment>());
        
        var validAppointmentAt = DateTime.Now.AddDays(1);
        var invalidPrice = Appointment.MaxPrice + 1.0;
        
        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => 
            _appointmentManager.AddAppointment(validAppointmentAt, invalidPrice, null, _patient, _physician));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("field Price must be between"));
        _mockAppointmentRepository.Verify(pr => pr.Create(It.IsAny<Appointment>()), Times.Never);
    }
    
    [Test]
    public void AddAppointment_ThrowsValidationException_WhenPhysicianIsNotAvailable()
    {
        // Arrange
        _mockAppointmentRepository.Setup(ar => ar.GetAppointmentsByPhysicianAndDate(It.IsAny<Guid>(), It.IsAny<DateTime>()))
            .Returns(new List<Appointment> {new Appointment(DateTime.Now.AddDays(1).AddHours(1), 100) { Physician = _physician }});
        
        var validAppointmentAt = DateTime.Now.AddDays(1);
        var validPrice = Appointment.MinPrice + 1.0;
        
        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => 
            _appointmentManager.AddAppointment(validAppointmentAt, validPrice, null, _patient, _physician));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("The physician already has an appointment at this date and time"));
        _mockAppointmentRepository.Verify(pr => pr.Create(It.IsAny<Appointment>()), Times.Never);
    }
}