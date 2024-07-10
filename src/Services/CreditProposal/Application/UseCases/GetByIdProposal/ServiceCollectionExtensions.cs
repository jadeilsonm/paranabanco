using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.GetByIdProposal;

public static class ServiceCollectionExtensions
{
    public static void AddGetByIdProposal(this IServiceCollection services)
    {
        services.AddScoped<IGetByIdProposalUseCase, GetByIdProposalUseCase>();
    }
}