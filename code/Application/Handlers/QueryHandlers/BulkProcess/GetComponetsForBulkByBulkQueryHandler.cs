using Application.Dto.Params.DynamicFormItem;
using Application.Interfaces.Repositories;
using Application.RequestModels.Extensions;
using AutoMapper;
using ConnectureOS.Framework.Helpers.Extensions;
using Domain.Entities.DynamicFormAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers.BulkProcess;

public class GetComponetsForBulkByBulkQueryRequest : IRequest<GetComponetsForBulkByBulkQueryResponse>
{
    public Int64 BulkId { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}

public class GetComponetsForBulkByBulkQueryResponse
{
    public PaginatedList<DynamicFormComponentRuleDto> Components { get; set; }
}


public class GetComponetsForBulkByBulkQueryHandler : IRequestHandler<GetComponetsForBulkByBulkQueryRequest, GetComponetsForBulkByBulkQueryResponse>
{
    private readonly IDynamicFormComponentRuleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetComponetsForBulkByBulkQueryHandler> _logger;
    public GetComponetsForBulkByBulkQueryHandler(IDynamicFormComponentRuleRepository repository, IMapper mapper, ILogger<GetComponetsForBulkByBulkQueryHandler> logger
   )
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetComponetsForBulkByBulkQueryResponse> Handle(GetComponetsForBulkByBulkQueryRequest request, CancellationToken cancellationToken)
    {

        try
        {
            var response = new GetComponetsForBulkByBulkQueryResponse();

            var components = await _repository.GetComponentsForBulkByBulkIdAsync(request.BulkId, cancellationToken);

            var countDynamicsForms = components
           .GroupBy(item => item.DynamicFormItemId)
           .ToDictionary(grp => grp.Key, grp => grp.Count());


            var result = components
          .GroupBy(item => new { item.ComponentName, item.DataType })
          .Where(grp => grp.Count() == countDynamicsForms.Count)
          .Select(grp => new DynamicFormComponentRule
          {

              ComponentName = grp.Key.ComponentName, 
              DataType = grp.Key.DataType
          })
          .ToList();



            var componentsDto = _mapper.Map<List<DynamicFormComponentRuleDto>>(result);

            response.Components = PaginatedList<DynamicFormComponentRuleDto>.Create(componentsDto, request.PageIndex, request.PageSize, string.Empty, string.Empty);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace);
            throw;
        }
    }
}

public class DynamicFormComponentRuleComparer : IEqualityComparer<DynamicFormComponentRule>
{
    public bool Equals(DynamicFormComponentRule x, DynamicFormComponentRule y)
    {
 
        return x.ComponentName == y.ComponentName
            && x.ComponentPropertyId == y.ComponentPropertyId
            && x.DataType == y.DataType;
    }

    public int GetHashCode(DynamicFormComponentRule obj)
    {
  
        return HashCode.Combine(obj.ComponentName, obj.ComponentPropertyId, obj.DataType);
    }
}