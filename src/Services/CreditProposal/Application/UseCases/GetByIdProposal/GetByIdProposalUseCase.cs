using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.GetByIdProposal;

public class GetByIdProposalUseCaseUseCase(ILogger<GetByIdProposalUseCaseUseCase> logger, ICreditProposalRepository creditProposalRepository)
    : IGetByIdProposalUseCase
{
    private readonly ILogger<GetByIdProposalUseCaseUseCase> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly ICreditProposalRepository _creditProposalRepository = creditProposalRepository ??
                                                                           throw new ArgumentNullException(nameof(creditProposalRepository));

    public async Task<CreditProposal> ExecuteAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            _logger.LogWarning("Invalid id");
            throw new BadRequestException("Invalid id");
        }
        
        _logger.LogInformation("Getting proposal with id {Id}", id);
        var result = await _creditProposalRepository.GetByIdAsync(id);
        
        if (result == null)
        {
            _logger.LogWarning("Proposal with id {Id} not found", id);
            throw new NotFoundException("Proposal not found");
        }
        
        _logger.LogInformation("Finished UseCase {UseCase}", nameof(GetByIdProposalUseCaseUseCase));
        return result;

    }
}