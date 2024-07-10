using Moq;
using FluentValidation;
using Xunit;
using Aplication.DTOs;
using Aplication.UseCases.Create;
using Aplication.UseCases.Producer;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Assert = Xunit.Assert;

namespace Register.Test;

public class CreateCustomerTests
{
    [Test]
    public async Task ExecuteAsync_ShouldCreateCustomer_WhenDataIsValid()
    {
        // Arrange
        var validatorMock = new Mock<IValidator<CustomerRequest>>();
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        var producerOnboardMock = new Mock<IProducerOnboard>();
        var loggerMock = new Mock<ILogger<CreateCustomer>>();
        var mailerSendGatewayMock = new Mock<IMailerSendGateway>();

        var createCustomer = new CreateCustomer(validatorMock.Object, customerRepositoryMock.Object, producerOnboardMock.Object, loggerMock.Object, mailerSendGatewayMock.Object);

        var customerRequest = new CustomerRequest("teste", "test@gmail.com", "123456", "123456", DateTime.Now, "1212212", 1200, 23456, new Address("teste", "pe", "Teste",  "12345678"));

        validatorMock.Setup(v => v.ValidateAsync(customerRequest, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
        customerRepositoryMock.Setup(c => c.IsExistCustomerWithEmail(customerRequest.Email)).ReturnsAsync(false);

        // Act
        var result = await createCustomer.ExecuteAsync(customerRequest);

        // Assert
        Assert.NotNull(result);
        customerRepositoryMock.Verify(c => c.AddCustomerAsync(It.IsAny<Customer>()), Times.Once);
        producerOnboardMock.Verify(p => p.Send(It.IsAny<CustomerEvent>(), default), Times.Once);
        mailerSendGatewayMock.Verify(m => m.SendMailAsync(It.IsAny<SendEmailBody>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowException_WhenDataIsInvalid()
    {
        // Arrange
        var validatorMock = new Mock<IValidator<CustomerRequest>>();
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        var producerOnboardMock = new Mock<IProducerOnboard>();
        var loggerMock = new Mock<ILogger<CreateCustomer>>();
        var mailerSendGatewayMock = new Mock<IMailerSendGateway>();

        var createCustomer = new CreateCustomer(validatorMock.Object, customerRepositoryMock.Object, producerOnboardMock.Object, loggerMock.Object, mailerSendGatewayMock.Object);

        var customerRequest = new CustomerRequest("teste", "test@gmail.com", "123456", "123456", DateTime.Now, "1212212", 1200, 23456, new Address("teste", "pe", "Teste",  "12345678"));

        validatorMock.Setup(v => v.ValidateAsync(customerRequest, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult(new[] { new FluentValidation.Results.ValidationFailure("Email", "Invalid email") }));

        // Act & Assert
        await Assert.ThrowsAsync<DataContractValidationException>(() => createCustomer.ExecuteAsync(customerRequest));
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowException_WhenCustomerAlreadyExists()
    {
        // Arrange
        var validatorMock = new Mock<IValidator<CustomerRequest>>();
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        var producerOnboardMock = new Mock<IProducerOnboard>();
        var loggerMock = new Mock<ILogger<CreateCustomer>>();
        var mailerSendGatewayMock = new Mock<IMailerSendGateway>();

        var createCustomer = new CreateCustomer(validatorMock.Object, customerRepositoryMock.Object, producerOnboardMock.Object, loggerMock.Object, mailerSendGatewayMock.Object);

        var customerRequest = new CustomerRequest("teste", "test@gmail.com", "123456", "123456", DateTime.Now, "1212212", 1200, 23456, new Address("teste", "pe", "Teste",  "12345678"));

        validatorMock.Setup(v => v.ValidateAsync(customerRequest, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
        customerRepositoryMock.Setup(c => c.IsExistCustomerWithEmail(customerRequest.Email)).ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => createCustomer.ExecuteAsync(customerRequest));
    }
}
