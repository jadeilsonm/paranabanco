namespace Application.UseCases.GetCreditCard;

public interface IGetCrediCardUseCase
{
    Task<IEnumerable<Core.Entities.CreditCard>> ExecuteAsync();
}