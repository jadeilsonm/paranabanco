using Application.UseCases;
using Application.UseCases.GetByIdProposal;
using Application.UseCases.Proposal;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddProposalUseCases();
        
        services.AddGetAllProposal();

        services.AddGetByIdProposal();
    }
}