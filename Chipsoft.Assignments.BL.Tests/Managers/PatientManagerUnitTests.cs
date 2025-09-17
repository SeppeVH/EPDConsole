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
[TestOf(typeof(PatientManager))]
public class PatientManagerUnitTests
{
    private PatientManager _patientManager;
    private Mock<IPatientRepository> _patientRepositoryMock;
    
    [SetUp]
    public void SetUp()
    {
        _patientRepositoryMock = new Mock<IPatientRepository>();
        _patientManager = new PatientManager(_patientRepositoryMock.Object);
    }

    [Test]
    public void GetAllPatients_ShouldCallReadAllOnce()
    {
        // Arrange
        _patientRepositoryMock.Setup(pr => pr.ReadAll()).Returns(new List<Domain.Patient>());
        
        // Act
        var result = _patientManager.GetAllPatients();
        
        // Assert
        _patientRepositoryMock.Verify(pr => pr.ReadAll(), Times.Once);
    }

    [Test]
    public void AddPatient_ThrowsValidationException_WhenNameIsEmpty()
    {
        // Arrange
        const string invalidName = "";
        const string validEmail = "test@mail.com";
        const string validAddress = "Main St 29, 10 ville, City";
        const string validPhoneNumber = "1234567890";
        var validBirthDate = new DateTime(1990, 1, 1);
        
        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => 
            _patientManager.AddPatient(invalidName, validEmail, validBirthDate, validAddress, validPhoneNumber));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("required"));
        _patientRepositoryMock.Verify(pr => pr.Create(It.IsAny<Patient>()), Times.Never);
    }
    
    [Test]
    public void AddPatient_ThrowsValidationException_WhenNameIsToLong()
    {
        // Arrange
        var invalidName = new string('a', User.MaxNameLength + 1);
        const string validEmail = "test@mail.com";
        const string validAddress = "Main St 29, 10 ville, City";
        const string validPhoneNumber = "1234567890";
        var validBirthDate = new DateTime(1990, 1, 1);
        
        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => 
            _patientManager.AddPatient(invalidName, validEmail, validBirthDate, validAddress, validPhoneNumber));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("maximum length"));
        _patientRepositoryMock.Verify(pr => pr.Create(It.IsAny<Patient>()), Times.Never);
    }
    
    [Test]
    public void AddPatient_ThrowsValidationException_WhenNameIsToShort()
    {
        // Arrange
        var invalidName = new string('a', User.MinNameLength - 1);
        const string validEmail = "test@mail.com";
        const string validAddress = "Main St 29, 10 ville, City";
        const string validPhoneNumber = "1234567890";
        var validBirthDate = new DateTime(1990, 1, 1);
        
        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => 
            _patientManager.AddPatient(invalidName, validEmail, validBirthDate, validAddress, validPhoneNumber));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("minimum length"));
        _patientRepositoryMock.Verify(pr => pr.Create(It.IsAny<Patient>()), Times.Never);
    }
    
    [Test]
    public void AddPatient_ThrowsValidationException_WhenInvalidEmail()
    {
        // Arrange
        var validName = new string('a', User.MinNameLength + 1);
        const string invalidEmail = "testmail.com";
        const string validAddress = "Main St 29, 10 ville, City";
        const string validPhoneNumber = "1234567890";
        var validBirthDate = new DateTime(1990, 1, 1);
        
        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => 
            _patientManager.AddPatient(validName, invalidEmail, validBirthDate, validAddress, validPhoneNumber));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("not a valid e-mail address"));
        _patientRepositoryMock.Verify(pr => pr.Create(It.IsAny<Patient>()), Times.Never);
    }
    
    [Test]
    public void AddPatient_ThrowsValidationException_WhenInvalidPhoneNumber()
    {
        // Arrange
        var validName = new string('a', User.MinNameLength + 1);
        const string validEmail = "test@mail.com";
        const string validAddress = "Main St 29, 10 ville, City";
        const string invalidPhoneNumber = "sfds12";
        var validBirthDate = new DateTime(1990, 1, 1);
        
        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => 
            _patientManager.AddPatient(validName, validEmail, validBirthDate, validAddress, invalidPhoneNumber));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("not a valid phone number"));
        _patientRepositoryMock.Verify(pr => pr.Create(It.IsAny<Patient>()), Times.Never);
    }
    
    [Test]
    public void AddPatient_ThrowsValidationException_WhenBirthdateInFuture()
    {
        // Arrange
        var validName = new string('a', User.MinNameLength + 1);
        const string validEmail = "test@mail.com";
        const string validAddress = "Main St 29, 10 ville, City";
        var invalidBirthDate = DateTime.Now.AddDays(1);
        
        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => 
            _patientManager.AddPatient(validName, validEmail, invalidBirthDate, validAddress));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("Birthdate must be in the past."));
        _patientRepositoryMock.Verify(pr => pr.Create(It.IsAny<Patient>()), Times.Never);
    }
    
    [Test]
    public void AddPatient_ThrowsValidationException_WhenInvalidAddress()
    {
        // Arrange
        var validName = new string('a', User.MinNameLength + 1);
        const string validEmail = "test@mail.com";
        const string invalidAddress = "Main St 29, City";
        var validBirthDate = new DateTime(1990, 1, 1);
        
        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => 
            _patientManager.AddPatient(validName, validEmail, validBirthDate, invalidAddress));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("Address must be in the correct format."));
        _patientRepositoryMock.Verify(pr => pr.Create(It.IsAny<Patient>()), Times.Never);
    }
    
    [Test]
    public void AddPatient_CallsCreateOnce_WhenDataIsValid()
    {
        // Arrange
        var validName = new string('a', User.MinNameLength + 1);
        const string validEmail = "test@mail.com";
        const string validAddress = "Main St 29, 10 ville, City";
        const string validPhoneNumber = "1234567890";
        var validBirthDate = new DateTime(1990, 1, 1);
        
        // Act
        var result = _patientManager.AddPatient(validName, validEmail, validBirthDate, validAddress, validPhoneNumber);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(validName));
        Assert.That(result.Email, Is.EqualTo(validEmail));
        Assert.That(result.Birthdate, Is.EqualTo(validBirthDate));
        Assert.That(result.Address, Is.EqualTo(validAddress));
        Assert.That(result.PhoneNumber, Is.EqualTo(validPhoneNumber));
        _patientRepositoryMock.Verify(pr => pr.Create(It.IsAny<Patient>()), Times.Once);
    }
}