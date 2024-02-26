// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using RulesEngine.Exceptions;
using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RulesEngine.HelperFunctions
{
    /// <summary>
    /// Helpers
    /// </summary>
    internal static class Helpers
    {
        private static dynamic isSuccess;

        internal static RuleFunc<RuleResultTree> ToResultTree(ReSettings reSettings, Rule rule, IEnumerable<RuleResultTree> childRuleResults, Func<object[], dynamic> isSuccessFunc, string exceptionMessage = "")
        //  internal static RuleFunc<RuleResultTree> ToResultTree(ReSettings reSettings, Rule rule, IEnumerable<RuleResultTree> childRuleResults, Func<object[], bool> isSuccessFunc, string exceptionMessage = "")
        {
            return (inputs) =>
            {

                //  var isSuccess = false;
                //dynamic isSucess;
                var inputsDict = new Dictionary<string, object>();
                try
                {
                    inputsDict = inputs.ToDictionary(c => c.Name, c => c.Value);
                    isSuccess = isSuccessFunc(inputs.Select(c => c.Value).ToArray());
                }
                catch (Exception ex)
                {
                    exceptionMessage = GetExceptionMessage($"Error while executing rule : {rule?.RuleName} - {ex.Message}", reSettings);
                    HandleRuleException(new RuleException(exceptionMessage, ex), rule, reSettings);
                    isSuccess = false;
                }

                Boolean realIsSuccess = false;
                //si el tipo de dato de  IsSuccess es distinto de booleano, entonces se convierte a booleano
                if (isSuccess.GetType().Name != typeof(Boolean).Name)
                {
                    realIsSuccess = true;
                    //if (isSuccess == "")
                    //{
                    //    realIsSuccess = false;
                    //}
                }
                else
                {
                    realIsSuccess = isSuccess;
                }


                return new RuleResultTree
                {
                    Rule = rule,
                    Inputs = inputsDict,
                    IsSuccess = realIsSuccess,
                    ResponseValue = isSuccess,
                    ChildResults = childRuleResults,
                    ExceptionMessage = exceptionMessage
                };

            };

        }

        internal static RuleFunc<RuleResultTree> ToRuleExceptionResult(ReSettings reSettings, Rule rule, Exception ex)
        {
            HandleRuleException(ex, rule, reSettings);
            return ToResultTree(reSettings, rule, null, (args) => false, ex.Message);
        }

        internal static void HandleRuleException(Exception ex, Rule rule, ReSettings reSettings)
        {
            ex.Data.Add(nameof(rule.RuleName), rule.RuleName);
            ex.Data.Add(nameof(rule.Expression), rule.Expression);

            if (!reSettings.EnableExceptionAsErrorMessage)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <param name="rule"></param>
        /// <param name="reSettings"></param>
        /// <returns></returns>
        internal static string GetExceptionMessage(string message, ReSettings reSettings)
        {
            return reSettings.IgnoreException ? "" : message;
        }
    }
}
