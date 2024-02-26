using Domain.Enums;
using RulesEngine.Models;

namespace Domain.Entities.RulesAggregate
{
    public class RootRules
    {
        public List<Workflow> workflowRules { get; set; }


        public RootRules()
        {
            workflowRules = new List<Workflow>();
        }

        public void AddWorkflowRule(RuleDynamic rule)
        {




            Workflow workflow = new Workflow();
            workflow.WorkflowName = rule.Name;
            workflow.RuleExpressionType = RuleExpressionType.LambdaExpression;
            List<Rule> rules = new List<Rule>();

            foreach (var action in rule.Actions)
            {
                if (action.ActionType == RuleType.Script)
                {
                    Rule newRule = new Rule();
                    newRule.RuleName = action.Name;
                    newRule.Enabled = true;

                    newRule.ErrorMessage = "0";
                    newRule.Expression = action.WhenScript;
                    newRule.RuleExpressionType = RuleExpressionType.LambdaExpression;

                    if (action.ThenScript.ToLower().Contains("common."))
                    {
                        newRule.SuccessEvent = "1";
                        newRule.Actions = new RuleActions()
                        {
                            //  OnFailure = new ActionInfo() { Name = "OutputExpression", Context = new Dictionary<string, object>() { { "Expression", action.ThenScript } } },
                            OnSuccess = new ActionInfo() { Name = "OutputExpression", Context = new Dictionary<string, object>() { { "Expression", action.ThenScript } } }

                        };

                    }
                    else
                    {

                        newRule.SuccessEvent = action.ThenScript;
                    }


                    rules.Add(newRule);

                }

            }

            workflow.Rules = rules;
            this.workflowRules.Add(workflow);

        }


    }
}
