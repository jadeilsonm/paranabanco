using Application.DTOs;
using Application.UseCases.Proposal;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;

namespace TestProject1;

public class ProposalUseCaseTests
{
    private readonly Mock<ILogger<ProposalUseCase>> _mockLogger;
    private readonly Mock<IValidator<OnboardingCustomeEvent>> _mockValidator;
    private readonly Mock<IMailerSendGateway> _mockMailerSendGateway;
    private readonly Mock<ICreditProposalRepository> _mockCreditProposalRepository;

    public ProposalUseCaseTests()
    {
        _mockLogger = new Mock<ILogger<ProposalUseCase>>();
        _mockValidator = new Mock<IValidator<OnboardingCustomeEvent>>();
        _mockMailerSendGateway = new Mock<IMailerSendGateway>();
        _mockCreditProposalRepository = new Mock<ICreditProposalRepository>();
    }

    [Fact]
    public async Task ExecuteAsync_CreatesProposal_WhenValidationPasses()
    {
        var onboardingEvent = new OnboardingCustomeEvent()
        {
            Id = Guid.NewGuid(), Email = "test@email.com", Name = "test", Salary = 10000, AmountAll = 89000,
            Document = "89210210", DateOfBirth = DateTime.Now, Address = new Address(), CellNumber = "1381273812"
        };
        _mockValidator.Setup(v => v.Validate(onboardingEvent)).Returns(new ValidationResult());

        var useCase = new ProposalUseCase(_mockLogger.Object, _mockValidator.Object, _mockMailerSendGateway.Object, _mockCreditProposalRepository.Object);

        await useCase.ExecuteAsync(onboardingEvent);

        _mockCreditProposalRepository.Verify(r => r.AddCustomerAsync(It.IsAny<CreditProposal>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ThrowsException_WhenValidationFails()
    {
        var onboardingEvent = new OnboardingCustomeEvent();
        _mockValidator.Setup(v => v.Validate(onboardingEvent)).Returns(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("TestProperty", "TestError") }));

        var useCase = new ProposalUseCase(_mockLogger.Object, _mockValidator.Object, _mockMailerSendGateway.Object, _mockCreditProposalRepository.Object);

        await Assert.ThrowsAsync<DataContractValidationException>(() => useCase.ExecuteAsync(onboardingEvent));
    }

    [Fact]
    public async Task ExecuteAsync_SendsEmail_WhenValidationPasses()
    {
        var onboardingEvent = new OnboardingCustomeEvent()
        {
            Id = Guid.NewGuid(), Email = "test@email.com", Name = "test", Salary = 10000, AmountAll = 89000,
            Document = "89210210", DateOfBirth = DateTime.Now, Address = new Address(), CellNumber = "1381273812"
        };
        _mockValidator.Setup(v => v.Validate(onboardingEvent)).Returns(new ValidationResult());

        var useCase = new ProposalUseCase(_mockLogger.Object, _mockValidator.Object, _mockMailerSendGateway.Object, _mockCreditProposalRepository.Object);

        await useCase.ExecuteAsync(onboardingEvent);

        _mockMailerSendGateway.Verify(m => m.SendMailAsync(It.IsAny<SendEmailBody>()), Times.Once);
    }
}