using RulesEngine.Actions;
using RulesEngine.Models;

namespace drx.cos.workflow.rulemanager.Services.Rules.HelperFunctions
{
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
       
            return "1"; 
        }


    }
}
