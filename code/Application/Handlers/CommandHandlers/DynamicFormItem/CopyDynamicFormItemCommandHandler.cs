using Application.Dto;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.RequestModels.CommandRequestModels.DynamicFormItem;
using Application.ResponseModels.CommandResponseModels.DynamicFormItem;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.DynamicFormItem
{
    public class CopyDynamicFormItemCommandHandler : IRequestHandler<CopyDynamicFormItemCommandRequest, CopyDynamicFormItemCommandResponse>
    {
        private readonly IDynamicFormService _dynamicFormService;
        private readonly ILogger<CopyDynamicFormItemCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IDynamicFormItemRepository _repository;



        public CopyDynamicFormItemCommandHandler(IDynamicFormService dynamicFormService, ILogger<CopyDynamicFormItemCommandHandler> logger, IMapper mapper, IDynamicFormItemRepository repository)
        {
            _dynamicFormService = dynamicFormService;
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CopyDynamicFormItemCommandResponse> Handle(CopyDynamicFormItemCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new CopyDynamicFormItemCommandResponse();

                var dynamicFormItem = await _repository.GetByIdAsync(request.DynamicFormItemId);

                var copy = await _dynamicFormService.CopyDynamicFormItem(dynamicFormItem, cancellationToken);

                response.DynamicFormItem = _mapper.Map<DynamicFormItemDto>(copy);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                throw ex;
            }
        }
    }
}
