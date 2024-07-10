using Moq;
using Microsoft.Extensions.Logging;
using Core.Entities;
using Core.Interfaces;
using Application.UseCases;
using Core.Exceptions;

namespace TestProject1;

public class GetAllProposalUseCaseTests
{
    private readonly Mock<ILogger<GetAllProposalUseCase>> _mockLogger;
    private readonly Mock<ICreditProposalRepository> _mockCreditProposalRepository;

    public GetAllProposalUseCaseTests()
    {
        _mockLogger = new Mock<ILogger<GetAllProposalUseCase>>();
        _mockCreditProposalRepository = new Mock<ICreditProposalRepository>();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsAllProposals_WhenProposalsExist()
    {
        // Arrange
        var proposals = new List<CreditProposal> { new CreditProposal(), new CreditProposal() };
        _mockCreditProposalRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(proposals);

        var useCase = new GetAllProposalUseCase(_mockLogger.Object, _mockCreditProposalRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync();

        // Assert
        Assert.Equal(proposals, result);
    }

    [Fact]
    public async Task ExecuteAsync_ThrowsNotFoundException_WhenNoProposalsExist()
    {
        // Arrange
        _mockCreditProposalRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(Enumerable.Empty<CreditProposal>());

        var useCase = new GetAllProposalUseCase(_mockLogger.Object, _mockCreditProposalRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => useCase.ExecuteAsync());
    }
}
