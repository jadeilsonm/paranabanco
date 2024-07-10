using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases;

public class GetAllProposalUseCase(
    ILogger<GetAllProposalUseCase> logger,
    ICreditProposalRepository creditProposalRepository)
    : IGetAllProposalUseCase
{
    private readonly ILogger<GetAllProposalUseCase> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly ICreditProposalRepository _creditProposalRepository = creditProposalRepository ??
                                                                           throw new ArgumentNullException(nameof(creditProposalRepository));

    public async Task<IEnumerable<CreditProposal>> ExecuteAsync()
    {
        _logger.LogInformation("Getting all proposals");

        var proposals = await _creditProposalRepository.GetAllAsync();
        
        if (proposals == null || !proposals.Any())
        {
            _logger.LogWarning("No proposals found");
            throw new NotFoundException("No proposals found");
        }

        _logger.LogInformation("Finished UseCase {UseCase}", nameof(GetAllProposalUseCase));

        return proposals;
    }
}