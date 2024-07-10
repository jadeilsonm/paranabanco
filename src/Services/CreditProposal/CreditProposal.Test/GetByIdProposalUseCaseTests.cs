using Application.UseCases.GetByIdProposal;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace TestProject1;

public class GetByIdProposalUseCaseTests
{
    private readonly Mock<ILogger<GetByIdProposalUseCase>> _mockLogger;
    private readonly Mock<ICreditProposalRepository> _mockCreditProposalRepository;

    public GetByIdProposalUseCaseTests()
    {
        _mockLogger = new Mock<ILogger<GetByIdProposalUseCase>>();
        _mockCreditProposalRepository = new Mock<ICreditProposalRepository>();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsProposal_WhenIdIsValidAndProposalExists()
    {
        var proposalId = Guid.NewGuid();
        var proposal = new CreditProposal();
        _mockCreditProposalRepository.Setup(repo => repo.GetByIdAsync(proposalId)).ReturnsAsync(proposal);

        var useCase = new GetByIdProposalUseCase(_mockLogger.Object, _mockCreditProposalRepository.Object);

        var result = await useCase.ExecuteAsync(proposalId);

        Assert.Equal(proposal, result);
    }

    [Fact]
    public async Task ExecuteAsync_ThrowsBadRequestException_WhenIdIsInvalid()
    {
        var invalidId = Guid.Empty;

        var useCase = new GetByIdProposalUseCase(_mockLogger.Object, _mockCreditProposalRepository.Object);

        await Assert.ThrowsAsync<BadRequestException>(() => useCase.ExecuteAsync(invalidId));
    }

    [Fact]
    public async Task ExecuteAsync_ThrowsNotFoundException_WhenIdIsValidAndProposalDoesNotExist()
    {
        var nonExistentId = Guid.NewGuid();
        _mockCreditProposalRepository.Setup(repo => repo.GetByIdAsync(nonExistentId)).ReturnsAsync((CreditProposal)null);

        var useCase = new GetByIdProposalUseCase(_mockLogger.Object, _mockCreditProposalRepository.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => useCase.ExecuteAsync(nonExistentId));
    }
}