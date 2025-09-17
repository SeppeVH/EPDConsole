using System;
using System.ComponentModel.DataAnnotations;
using Chipsoft.Assignments.BL.Managers;
using Chipsoft.Assignments.DAL.Repositories.Interfaces;
using Chipsoft.Assignments.Domain;
using Moq;
using NUnit.Framework;

namespace Chipsoft.Assignments.BL.Tests.Managers;

[TestFixture]
[TestOf(typeof(PhysicianManager))]
public class PhysicianManagerUnitTests
{
    private PhysicianManager _physicianManager;
    private Mock<IPhysicianRepository> _mockPhysicianRepository;

    [SetUp]
    public void SetUp()
    {
        _mockPhysicianRepository = new Mock<IPhysicianRepository>();
        _physicianManager = new PhysicianManager(_mockPhysicianRepository.Object);
    }
    
    [Test]
    public void AddPhysician_ThrowsValidationException_WhenInvalidWorkFloor()
    {
        // Arrange
        var validName = new string('a', User.MinNameLength + 1);
        const string validEmail = "test@mail.com";
        const string invalidWorkFloor = "A";
        var validHiredAt = DateTime.Now.AddYears(-1);
        
        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => 
            _physicianManager.AddPhysician(validName, validEmail, invalidWorkFloor, validHiredAt));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("Work floor must be in the format"));
        _mockPhysicianRepository.Verify(pr => pr.Create(It.IsAny<Physician>()), Times.Never);
    }
    
    [Test]
    public void AddPhysician_ThrowsValidationException_WhenHireAtInFuture()
    {
        // Arrange
        var validName = new string('a', User.MinNameLength + 1);
        const string validEmail = "test@mail.com";
        const string validWorkFloor = "1A";
        var invalidHiredAt = DateTime.Now.AddDays(1);
        
        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => 
            _physicianManager.AddPhysician(validName, validEmail, validWorkFloor, invalidHiredAt));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("Hired date cannot be in the future"));
        _mockPhysicianRepository.Verify(pr => pr.Create(It.IsAny<Physician>()), Times.Never);
    }
    
    [Test]
    public void AddPhysician_CallsCreateOnce_WhenDataIsValid()
    {
        // Arrange
        var validName = new string('a', User.MinNameLength + 1);
        const string validEmail = "test@mail.com";
        const string validWorkFloor = "1A";
        var validHiredAt = DateTime.Now.AddYears(-1);
        
        // Act
        var result = _physicianManager.AddPhysician(validName, validEmail, validWorkFloor, validHiredAt);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(validName));
        Assert.That(result.Email, Is.EqualTo(validEmail));
        Assert.That(result.WorkFloor, Is.EqualTo(validWorkFloor));
        Assert.That(result.HiredAt, Is.EqualTo(validHiredAt));
        _mockPhysicianRepository.Verify(pr => pr.Create(It.IsAny<Physician>()), Times.Once);
    }
}