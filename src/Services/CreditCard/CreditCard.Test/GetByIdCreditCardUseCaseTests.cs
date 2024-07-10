using Application.UseCases.GetByIdCreditCard;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace CreditCard.Test;

public class GetByIdCreditCardUseCaseTests
{
    private Mock<ILogger<GetByIdCreditCardUseCase>> _loggerMock;
    private Mock<ICreditCardRepository> _creditCardRepositoryMock;
    private GetByIdCreditCardUseCase _getByIdCreditCardUseCase;

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<GetByIdCreditCardUseCase>>();
        _creditCardRepositoryMock = new Mock<ICreditCardRepository>();

        _getByIdCreditCardUseCase = new GetByIdCreditCardUseCase(
            _loggerMock.Object,
            _creditCardRepositoryMock.Object);
    }

    [Test]
    public void ExecuteAsync_InvalidId_ThrowsBadRequestException()
    {
        // Arrange
        var id = Guid.Empty;

        // Act & Assert
        Assert.ThrowsAsync<BadRequestException>(() => _getByIdCreditCardUseCase.ExecuteAsync(id));
    }

    [Test]
    public void ExecuteAsync_CreditCardNotFound_ThrowsNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();
        _creditCardRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Core.Entities.CreditCard)null);

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _getByIdCreditCardUseCase.ExecuteAsync(id));
    }

    [Test]
    public async Task ExecuteAsync_ValidId_ReturnsCreditCard()
    {
        // Arrange
        var id = Guid.NewGuid();
        var creditCard = new Core.Entities.CreditCard()
        {
            Email = "test@gmail.com",
            Name = "Test",
            CardNumber = "1234567890123456",
            Salary = 1000,
            Limit = 2902,
            ExpirationDate = DateTime.Now.AddYears(5),
            CreatedAt = DateTime.Now,
            CustomerId = Guid.NewGuid(),
            Password = "test@password"
        };
        _creditCardRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(creditCard);

        // Act
        var result = await _getByIdCreditCardUseCase.ExecuteAsync(id);

        // Assert
        Assert.AreEqual(creditCard, result);
    }
}