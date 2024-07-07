using Application.DTOs;
using Application.UseCases.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.Proposal;

public static class ServiceCollectionExtension
{
    public static void AddProposalUseCases(this IServiceCollection services)
    {
        services.AddSingleton<IValidator<OnboardingCustomeEvent>, OnboardingValidate>();
        
        services.AddSingleton<IProposalUseCase, ProposalUseCase>();
    }
}