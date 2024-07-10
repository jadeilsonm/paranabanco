using Core.Exceptions;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.GetCreditCard;

public class GetCreditCardUseCase(ILogger<GetCreditCardUseCase> logger, ICreditCardRepository creditCardRepository)
    : IGetCrediCardUseCase
{
    private readonly ILogger<GetCreditCardUseCase> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly ICreditCardRepository _creditCardRepository = creditCardRepository ?? throw new ArgumentNullException(nameof(creditCardRepository));
    
    public async Task<IEnumerable<Core.Entities.CreditCard>> ExecuteAsync()
    {
        IEnumerable<Core.Entities.CreditCard> result = await _creditCardRepository.GetAllAsync();
        if (result == null || !result.Any())
        {
            _logger.LogWarning("No credit card found");
            throw new NotFoundException("No credit card found");
        }

        return result;
    }
}