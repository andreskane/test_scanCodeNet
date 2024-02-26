using Domain.Entities.Common;
using Domain.Entities.RulesAggregate;
using Microsoft.Extensions.Logging;
using RulesEngine.Models;
using System.Dynamic;

namespace Application.Services.Rules;


public interface IProcessRuleEngine
{
    Task<ExecuteRuleResponse> Execute(List<Workflow> workflowRules, RuleDynamic rule, List<KeyValue> paramInput, ReSettings reSettings, CancellationToken cancellationToken);
}
public class ProcessRuleEngine : IProcessRuleEngine
{

    private readonly ILogger<ProcessRuleEngine> _logger;

    public ProcessRuleEngine(

                              ILogger<ProcessRuleEngine> logger

                   )
    {

        _logger = logger;


    }

    public async Task<ExecuteRuleResponse> Execute(List<Workflow> workflowRules, RuleDynamic rule, List<KeyValue> paramInput, ReSettings reSettings, CancellationToken cancellationToken)
    {

        var response = new ExecuteRuleResponse();

        var workflows = workflowRules;

        var bre = new RulesEngine.RulesEngine(workflows.ToArray(), reSettings);

        dynamic datas = new ExpandoObject();

        var ListParametresRule = rule.Actions.Select(x => x.Parameters).ToList();

        var InputNestedParametres = ListParametresRule.SelectMany(x => x).Where(x => x.DataType == "input").Select(x => x.Name).Distinct().ToList();

        foreach (var param in InputNestedParametres)
        {
            var aux = paramInput.Where(x => x.Key == param).FirstOrDefault();
            if (aux != null)



                if (Int64.TryParse(aux.Value.ToString(), out Int64 number))
                {
                    ((IDictionary<string, object>)datas).Add(param, Convert.ToInt64(aux.Value.ToString()));
                }
                else
                {
                    ((IDictionary<string, object>)datas).Add(param, aux.Value.ToString());
                }


            else
                ((IDictionary<string, object>)datas).Add(param, "");
        }



        var inputs = new dynamic[]
          {
                datas
          };

        bool outcome = false;
        response.ParamOutPut = new List<KeyValue>();
        foreach (var work in workflows)
        {
            var resultList = bre.ExecuteAllRulesAsync(work.WorkflowName, inputs).Result;


            for (var i = 0; i < resultList.Count; i++)
            {
                if (resultList[i].IsSuccess)
                {

                    if (resultList[i].ActionResult.Output != null)
                    {
                        var paramOut = rule.Actions[i].Parameters.Where(x => x.DataType == "output").FirstOrDefault();
                        response.ParamOutPut.Add(item: new KeyValue { Key = paramOut.Name.ToString(), Value = resultList[i].ActionResult.Output.ToString() });
                    }
                    else
                    {
                        var paramOut = rule.Actions[i].Parameters.Where(x => x.DataType == "output").FirstOrDefault();
                        response.ParamOutPut.Add(item: new KeyValue { Key = paramOut.Name.ToString(), Value = resultList[i].Rule.SuccessEvent });
                    }
                }
                else
                {
                    var paramOut = rule.Actions[i].Parameters.Where(x => x.DataType == "output").FirstOrDefault();
                    response.ParamOutPut.Add(item: new KeyValue { Key = paramOut.Name.ToString(), Value = false });
                }

            }

        }
        List<KeyValue> ParamOutputTemp = new List<KeyValue>();

        for (int i = 0; i < response.ParamOutPut.Count; i++)
        {
            var aux = response.ParamOutPut.Where(x => x.Key == response.ParamOutPut[i].Key).ToList();
            if (aux.Count > 1)
            {


                var paramToAdd = response.ParamOutPut.Where(x => x.Value.ToString() != "false").ToList()[0];
                foreach (var pp in response.ParamOutPut)
                {
                    if (pp.Value.ToString().ToLower() != "false")
                    {
                        paramToAdd = pp;
                    }

                }

                ParamOutputTemp.Add(paramToAdd);
            }
            else
            {
                ParamOutputTemp.Add(response.ParamOutPut[i]);
            }
        }

        response.ParamOutPut = ParamOutputTemp.GroupBy(item => item.Key).Select(group => group.First()).ToList();
        return response;



    }


}