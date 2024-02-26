using Application.Dto;
using Application.Interfaces;
using Application.RequestModels.QueriesRequestModels;
using Application.ResponseModels.QueriesResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers
{
    public class GetDFProductAttributesByIdQueryHandler : IRequestHandler<GetDynamicFormProductAttributesByIdQueryRequest, GetDynamicFormProductAttributesByIdQueryResponse>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormProductAttributes> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDFProductAttributesByIdQueryHandler> _logger;
        public GetDFProductAttributesByIdQueryHandler(IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormProductAttributes> repository, IMapper mapper, ILogger<GetDFProductAttributesByIdQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetDynamicFormProductAttributesByIdQueryResponse> Handle(GetDynamicFormProductAttributesByIdQueryRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var response = new GetDynamicFormProductAttributesByIdQueryResponse();

                var workflow = await _repository.GetByIdAsync(request.Id);
                var workflowDto = _mapper.Map<DynamicFormProductAttributeDto>(workflow);

                response.WorkflowProductAttribute = workflowDto;

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
