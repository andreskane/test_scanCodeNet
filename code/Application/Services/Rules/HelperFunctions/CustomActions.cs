using RulesEngine.Actions;
using RulesEngine.Models;

namespace Application.Services.Rules;
public class CustomActions
{
}


public class CallApi : ActionBase
{

    public CallApi(string someInput)
    {

    }

    public override async ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters)
    {
        var customInput = context.GetContext<string>("Expression");
        //Add your custom logic here
        return "1";// await MyCustomLogicAsync();
    }


}

