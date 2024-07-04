using Aplication.DTOs;
using Aplication.UseCases.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Aplication.UseCases.Create;

public static class ServiceCollectionExtensions
{
    public static void AddCustomerUseCases(this IServiceCollection services)
    {
        // Add Validators
        services.AddScoped<IValidator<CustomerRequest>, CustomerValidator>();
        
        // Add UseCases
        services.AddScoped<ICreateCustomer, CreateCustomer>();
    }
}