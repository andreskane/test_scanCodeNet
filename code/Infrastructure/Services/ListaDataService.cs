using Application.Dto;
using Application.Interfaces.Repositories;
using Application.Services;
using AutoMapper;
 
using Domain.Entities.ListAggregate;
using Microsoft.Extensions.Logging;
using System.Threading;


namespace Infrastructure.Services;

public interface IListaDataService {

    Task <IEnumerable<ListValue>> GetDataFromList(String ListName);
    Task<IEnumerable<ListValue>> GetDataFromList(String ListName,string key);
    Task<IEnumerable<ListValue>> GetKeyFromList(String ListName, string Value);
}

public class ListaDataService: IListaDataService
{
 
 
    private readonly IMapper _mapper;
    private readonly ILogger<DynamicFormService> _logger;
    private readonly IListValueRepository _repository;

    

    public ListaDataService(
        IMapper mapper,
        ILogger<DynamicFormService> logger,
        IListValueRepository repository
        )
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repository;

    }

    public async Task<IEnumerable<ListValue>> GetDataFromList(string ListName)
    {
        var response = new List<ListValue>();
        var pagedList = await _repository.GetByListNameAsync(ListName, default);

        
        return pagedList;
    }
    public async Task<IEnumerable<ListValue>> GetDataFromList(string ListName,string key)
    {
        var response = new List<ListValue>();
        var pagedList = await _repository.GetByListNameAsync(ListName, key, default);


        return pagedList;
    }

    public async Task<IEnumerable<ListValue>> GetKeyFromList(string ListName, string Value)
    {
        var response = new List<ListValue>();
        var pagedList = await _repository.GetKeyByListNameAsync(ListName, Value, default);


        return pagedList;
    }
}
