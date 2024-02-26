using Application.Dto;
using Application.Interfaces.Documental;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.Handlers.QueryHandlers
{

    public class GetDynamicFormItemByWorkflowIdQueryHandler : IRequestHandler<GetWDynamicFormByDynamicFormIdRequest, GetWDynamicFormByDynamicFormIdResponse>
    {
        private readonly IDynamicFormItemRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDynamicFormItemByIdQueryHandler> _logger;
        private readonly IDocDynamicFormRepository _docDynamicFormRepository;

        public GetDynamicFormItemByWorkflowIdQueryHandler(IDynamicFormItemRepository repository, IMapper mapper,
             IDocDynamicFormRepository docDynamicFormRepository,
            ILogger<GetDynamicFormItemByIdQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _docDynamicFormRepository = docDynamicFormRepository;
        }

        public async Task<GetWDynamicFormByDynamicFormIdResponse> Handle(GetWDynamicFormByDynamicFormIdRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var response = new GetWDynamicFormByDynamicFormIdResponse();

                var workflow = await _repository.GetDynamicByWorkflowID(request.Id, cancellationToken);
                var workflowDto = _mapper.Map<DynamicFormItemDto>(workflow);

                //layout get from documental
                var layout = await _docDynamicFormRepository.GetDynamicFormByKey(workflowDto.CodeFlow);

                var destino = _mapper.Map<RootObjectDto>(layout);

                workflowDto.Layout = JsonConvert.SerializeObject(layout.Pages);
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

    public class GetWDynamicFormByDynamicFormIdResponse
    {
        public DynamicFormItemDto WDynamicForm { get; set; }
    }

    public class GetWDynamicFormByDynamicFormIdRequest : IRequest<GetWDynamicFormByDynamicFormIdResponse>
    {
        [DataMember]
        [Required(ErrorMessage = "WorkflowDynamicForm Id is required.")]
        public Int64 Id { get; set; }
    }
}
