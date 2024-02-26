using Application.Interfaces.Repositories;
using Application.Services;
using AutoMapper;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace drx.cos.workflow.rulemanager.Services.Rules.HelperFunctions;

public static class Common
{
    private static IListaDataService _listaDataService;

    public static void InitializeListaDataServiceLambda(IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();


        var mapper = serviceProvider.GetRequiredService<IMapper>();
        var logger = serviceProvider.GetRequiredService<ILogger<DynamicFormService>>();
        var repository = serviceProvider.GetRequiredService<IListValueRepository>();

        
        _listaDataService = new ListaDataService(logger, repository);
    }

    private static DbContextOptions<ApplicationDbContext> _options;
    public static void Initialize(DbContextOptions<ApplicationDbContext> options)
    {
        _options = options;
    }


    public static bool CheckContains(string check, string valList)
    {
        if (string.IsNullOrEmpty(check) || string.IsNullOrEmpty(valList))
            return false;

        var list = valList.Split(',').ToList();
        return list.Contains(check);
    }

    public static dynamic CheckContainsInDataList(string check, string list)
    {
        var checkString = check.ToString();
        var result = _listaDataService.GetDataFromList(list, checkString).Result;

        if (result == null)
            return false;


        return true;

    }

    public static dynamic CheckContainsInDataList(Int64 check, string list)
    {
        var checkString = check.ToString();
        var result = _listaDataService.GetDataFromList(list, checkString).Result;

        if (result == null)
            return false;


        return true;

    }


    public static dynamic GetKeyFromValueInDataList(string value, string list)
    {

        var result = _listaDataService.GetKeyFromList(list, value).Result;

        if (result == null)

        { return false; }
        else
        {

            if (result.Count() == 0)
            {
                return false;
            }
        }

        return result.FirstOrDefault().Key;


    }


    private static dynamic GetValue(dynamic key, string list)
    {


        var keyString = key.ToString();
        var result = _listaDataService.GetDataFromList(list, keyString).Result;

        if (result == null)

        { return false; }
        else
        { return result[0].Value; }
    }

    public static dynamic GetValueInDataList(dynamic key, string list, string? addText = "")
    {



        var keyString = key.ToString();
        var result = _listaDataService.GetDataFromList(list, keyString).Result;

        if (result == null)

        { return false; }
        else
        {


            if (result.Count == 0)
            {
                return false;
            }
        }

        return string.Format("{0}{1}", addText, result[0].Value);


    }

    public static dynamic GetValueInDataList(dynamic key, string list)
    {
        var keyString = key.ToString();
        var result = _listaDataService.GetDataFromList(list, keyString).Result;

        if (result == null)

        { return false; }
        else
        {


            if (result.Count == 0)
            {
                return false;
            }
        }

        return result[0].Value;


    }



}
