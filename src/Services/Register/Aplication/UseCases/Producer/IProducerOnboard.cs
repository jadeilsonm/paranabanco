using Aplication.DTOs;

namespace Aplication.UseCases.Producer;

public interface IProducerOnboard
{
    public void Send(CustomerEvent message, CancellationToken cancellationToken = default);
}