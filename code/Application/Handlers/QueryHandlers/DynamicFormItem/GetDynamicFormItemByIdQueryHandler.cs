using Application.Dto;
using Application.Interfaces.Repositories;
using Application.RequestModels.QueriesRequestModels;
using Application.ResponseModels.QueriesResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers
{

    public class GetDynamicFormItemByIdQueryHandler : IRequestHandler<GetDynamicFormItemByIdQueryRequest, GetDynamicFormItemByIdQueryResponse>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormItem> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDynamicFormItemByIdQueryHandler> _logger;
        public GetDynamicFormItemByIdQueryHandler(IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormItem> repository, IMapper mapper, ILogger<GetDynamicFormItemByIdQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetDynamicFormItemByIdQueryResponse> Handle(GetDynamicFormItemByIdQueryRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var response = new GetDynamicFormItemByIdQueryResponse();

                var workflow = await _repository.GetByIdAsync(request.Id);
                var workflowDto = _mapper.Map<DynamicFormItemDto>(workflow);

                response.WDynamicForm = workflowDto;

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
