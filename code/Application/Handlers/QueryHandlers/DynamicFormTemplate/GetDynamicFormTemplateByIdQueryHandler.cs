using Application.Dto;
using Application.Interfaces.Repositories;
using Application.RequestModels.QueriesRequestModels;
using Application.ResponseModels.QueriesResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers
{
    internal class GetDynamicFormTemplateByIdQueryHandler : IRequestHandler<GetDynamicFormTemplateByIdQueryRequest, GetDynamicFormTemplateByIdQueryResponse>
    {
        private readonly IDynamicFormTemplateRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDynamicFormTemplateByIdQueryHandler> _logger;
        public GetDynamicFormTemplateByIdQueryHandler(IDynamicFormTemplateRepository repository, IMapper mapper, ILogger<GetDynamicFormTemplateByIdQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetDynamicFormTemplateByIdQueryResponse> Handle(GetDynamicFormTemplateByIdQueryRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var response = new GetDynamicFormTemplateByIdQueryResponse();

                var workflow = await _repository.GetCompleteByAsync(request.Id);
                var workflowDto = _mapper.Map<DynamicFormTemplateDto>(workflow);

                response.DynamicFormTemplate = workflowDto;

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
