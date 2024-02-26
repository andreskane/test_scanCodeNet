using Application.Dto;
using Application.Interfaces.Documental;
using Application.Interfaces.Repositories;
using Application.RequestModels.QueriesRequestModels;
using Application.ResponseModels.QueriesResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Handlers.QueryHandlers
{
    public class GetLastPublishedDynamicFormItemQueryHandler : IRequestHandler<GetLastPublishedDynamicFormItemRequest, GetDynamicFormItemResponse>
    {
        private readonly IDynamicFormItemRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetLastPublishedDynamicFormItemQueryHandler> _logger;
        private readonly IDocDynamicFormRepository _docDynamicFormRepository;
        public GetLastPublishedDynamicFormItemQueryHandler(IDynamicFormItemRepository repository,
            IMapper mapper,
            ILogger<GetLastPublishedDynamicFormItemQueryHandler> logger,
            IDocDynamicFormRepository docDynamicFormRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _docDynamicFormRepository = docDynamicFormRepository;
        }

        public async Task<GetDynamicFormItemResponse> Handle(GetLastPublishedDynamicFormItemRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var response = new GetDynamicFormItemResponse();

                var workflow = await _repository.GetLastPublishedWorkflowDynamicForm(request.ProductId, request.ZipCode, cancellationToken);
                var workflowDto = _mapper.Map<DynamicFormItemDto>(workflow);

                if (workflowDto == null)
                    return response;
                var layout = await _docDynamicFormRepository.GetDynamicFormByKey(workflowDto.CodeFlow);
                workflowDto.Layout = JsonConvert.SerializeObject(layout.Pages);


                workflowDto.Layout = workflow.Layout;
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
