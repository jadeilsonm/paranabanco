using Core.Exceptions;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.GetByIdCreditCard;

public class GetByIdCreditCardUseCase(ILogger<GetByIdCreditCardUseCase> logger, ICreditCardRepository creditCardRepository) : IGetByIdCreditCardUseCase
{
    private readonly ILogger<GetByIdCreditCardUseCase> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly ICreditCardRepository _creditCardRepository = creditCardRepository ?? throw new ArgumentNullException(nameof(creditCardRepository));
    
    public async Task<Core.Entities.CreditCard> ExecuteAsync(Guid id)
    {
        
        if (id == Guid.Empty)
        {
            _logger.LogWarning("Invalid id");
            throw new BadRequestException("Invalid id");
        }
        Core.Entities.CreditCard result = await _creditCardRepository.GetByIdAsync(id);
        if (result == null)
        {
            _logger.LogWarning("No credit card found");
            throw new NotFoundException("No credit card found");
        }

        return result;
    }
}