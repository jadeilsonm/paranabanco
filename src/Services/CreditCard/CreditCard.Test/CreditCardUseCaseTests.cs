using Moq;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Application.UseCases.CreditCard;
using Core.Interfaces;
using Application.DTOs;
using FluentValidation.Results;

namespace CreditCard.Test;

    public class CreditCardUseCaseTests
    {
        private Mock<IValidator<CreditCardEvent>> _validatorMock;
        private Mock<ILogger<CreditCardEvent>> _loggerMock;
        private Mock<IMailerSendGateway> _mailerSendGatewayMock;
        private Mock<ICreditCardRepository> _creditCardRepositoryMock;
        private CreditCardUseCase _creditCardUseCase;

        [SetUp]
        public void Setup()
        {
            _validatorMock = new Mock<IValidator<CreditCardEvent>>();
            _loggerMock = new Mock<ILogger<CreditCardEvent>>();
            _mailerSendGatewayMock = new Mock<IMailerSendGateway>();
            _creditCardRepositoryMock = new Mock<ICreditCardRepository>();

            _creditCardUseCase = new CreditCardUseCase(
                _validatorMock.Object,
                _loggerMock.Object,
                _mailerSendGatewayMock.Object,
                _creditCardRepositoryMock.Object);
        }

        [Test]
        public async Task ExecuteAsync_ValidCreditCardEvent_CallsAddCustomerAsync()
        {
            // Arrange
            var creditCardEvent = new CreditCardEvent { /* initialize properties here */ };
            _validatorMock.Setup(v => v.Validate(creditCardEvent)).Returns(new ValidationResult());

            // Act
            await _creditCardUseCase.ExecuteAsync(creditCardEvent);

            // Assert
            _creditCardRepositoryMock.Verify(r => r.AddCustomerAsync(It.IsAny<Core.Entities.CreditCard>()), Times.Once);
        }
    }
