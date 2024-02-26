using Coravel.Queuing.Interfaces;
using MediatR;

namespace Application.Handlers.CommandHandlers
{


    public class OrchestationStatusDTO
    {
        public String SequenceHubId { get; set; } = String.Empty;

    }

    public class ProcessBulkCommand : IRequest<OrchestationStatusDTO>
    {
    }

    public class CalculateDashboardCommandHandler : IRequestHandler<ProcessBulkCommand, OrchestationStatusDTO>
    {

        private readonly IQueue _queue;

        public const string CalculateDashboard = "CalculateDashboard";

        public CalculateDashboardCommandHandler(IQueue queue)
        {
            _queue = queue;
        }

        public async Task<OrchestationStatusDTO> Handle(ProcessBulkCommand request, CancellationToken cancellationToken)
        {

            return new OrchestationStatusDTO();
        }
    }
}
