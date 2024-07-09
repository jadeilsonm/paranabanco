using System.Globalization;
using Core.Entities;

namespace Application.DTOs;

public static class CreditCardExtensions
{
    public static CreditCard MapToCreditCard(this CreditCardEvent creditCardEvent, double limit, string cardNumber, string password, string expirationDate)
    {
        return new CreditCard
        {
            Id = Guid.NewGuid(),
            CustomerId = creditCardEvent.Id,
            Name = creditCardEvent.Name,
            Email = creditCardEvent.Email,
            Salary = creditCardEvent.Salary,
            Limit = limit,
            CardNumber = cardNumber,
            Password = password,
            ExpirationDate = DateTime.Now.AddYears(5),
            CreatedAt = DateTime.Now
        };
    }
}