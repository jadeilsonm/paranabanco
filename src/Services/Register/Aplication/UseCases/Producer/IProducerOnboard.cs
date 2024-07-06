using Aplication.DTOs;

namespace Aplication.UseCases.Producer;

public interface IProducerOnboard
{
    public void Send(CustomerResponse message, CancellationToken cancellationToken = default);
}