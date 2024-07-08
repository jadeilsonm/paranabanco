using Application.DTOs;

namespace Application.UseCases.CreditCard;

public interface ICreditCardUseCase
{
    Task ExecuteAsync(CreditCardEvent creditCardEvent);
}