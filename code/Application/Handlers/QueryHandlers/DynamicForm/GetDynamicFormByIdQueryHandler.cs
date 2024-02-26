using Application.Dto;
using Application.Interfaces.Repositories;
using Application.RequestModels.QueriesRequestModels;
using Application.ResponseModels.QueriesResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers
{
    public class GetDynamicFormByIdQueryHandler : IRequestHandler<GetDynamicFormByIdQueryRequest, GetDynamicFormByIdQueryResponse>
    {
        private readonly IDynamicFormRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDynamicFormByIdQueryHandler> _logger;
        public GetDynamicFormByIdQueryHandler(IDynamicFormRepository repository, IMapper mapper, ILogger<GetDynamicFormByIdQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetDynamicFormByIdQueryResponse> Handle(GetDynamicFormByIdQueryRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var response = new GetDynamicFormByIdQueryResponse();

                var workflow = await _repository.GetByIdAsync(request.Id);
                var workflowDto = _mapper.Map<DynamicFormDto>(workflow);

                response.Workflow = workflowDto;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }
    }
}
