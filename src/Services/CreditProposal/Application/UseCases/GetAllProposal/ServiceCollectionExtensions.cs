using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases;

public static class ServiceCollectionExtensions
{
    public static void AddGetAllProposal(this IServiceCollection services)
    {
        services.AddScoped<IGetAllProposalUseCase, GetAllProposalUseCase>();
    }
}