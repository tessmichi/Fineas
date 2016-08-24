using Fineas.Controllers;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Fineas.Dialogs
{
    public class QueryDialog
    {
        public static IDialog<QueryForm> BuildDialog(string expenseCategory, string timePeriod)
        {
            /*QueryForm startForm = new QueryForm()
            {
                ExpenseCategory = expenseMatch,
                TimePeriod = timeMatch
            };*/
            return new FormDialog<QueryForm>(new QueryForm(), QueryForm.BuildForm, FormOptions.PromptInStart);
        }
    }

    [Serializable]
    public class QueryForm
    {
        public static IForm<QueryForm> BuildForm()
        {
            return new FormBuilder<QueryForm>()
                .Field(new FieldReflector<QueryForm>(nameof(ExpenseCategory))
                    .SetType(null)
                    .SetDefine((state, field) => DefineDelegate(state, field, DataRetriever.LineItemDescriptions.Keys.ToList())))
                .Field(new FieldReflector<QueryForm>(nameof(TimePeriod))
                    .SetType(null)
                    .SetDefine((state, field) => DefineDelegate(state, field, DataRetriever.TimeframeOptions.ToList()))
                )
                .AddRemainingFields()
                .Build();
        }

        private static Task<bool> DefineDelegate(QueryForm state, Field<QueryForm> field, List<string> choices)
        {
            foreach (var choice in choices)
            {
                field.AddDescription(choice, choice).AddTerms(choice, choice);
            }

            return Task.FromResult(true);
        }

        public string ExpenseCategory { get; set; }
        public string TimePeriod { get; set; }
    }
}