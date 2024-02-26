using Application.Dto;
using Application.Dto.Params.ListValue;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers.ListValue
{
    public class GetAllDynamicListHandler : IRequestHandler<GetAllDynamicListRequest, GetAllDynamicListResponse>
    {

        private readonly IListValueRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllDynamicListHandler> _logger;

        public GetAllDynamicListHandler(IListValueRepository repository, IMapper mapper, ILogger<GetAllDynamicListHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<GetAllDynamicListResponse> Handle(GetAllDynamicListRequest request, CancellationToken cancellationToken)
        {
            var response = new GetAllDynamicListResponse();

            var res = await _repository.GetAllListDefinitionAsync();
            response.ListDefinition = _mapper.Map<List<ListDefinitionDto>>(res);


            return response;
        }
    }

    public class GetAllDynamicListResponse
    {
        public List<ListDefinitionDto> ListDefinition { get; set; }
    }

    public class GetAllDynamicListRequest : IRequest<GetAllDynamicListResponse>
    {
        public ListValueFilter Filter { get; set; }
    }
}
