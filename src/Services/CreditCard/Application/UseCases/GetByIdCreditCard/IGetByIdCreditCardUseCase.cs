namespace Application.UseCases.GetByIdCreditCard;

public interface IGetByIdCreditCardUseCase
{
    Task<Core.Entities.CreditCard> ExecuteAsync(Guid id);
}