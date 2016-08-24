// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas.Dialogs
{
    using Controllers;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.FormFlow;
    using Microsoft.Bot.Builder.FormFlow.Advanced;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class QueryDialog
    {
        public static IDialog<QueryForm> BuildDialog(string expenseCategory, string timePeriod)
        {
            string expenseMatch = ClosestMatch(DataRetriever.LineItemDescriptions.Keys.ToArray(), expenseCategory);
            string timeMatch = BestTimeMatch(timePeriod);
            QueryForm startForm = new QueryForm()
            {
                ExpenseCategory = expenseMatch,
                TimePeriod = timeMatch
            };
            return new FormDialog<QueryForm>(startForm, QueryForm.BuildForm, FormOptions.PromptInStart);
        }

        // TODO: Better time matching... or don't separate it
        private static string BestTimeMatch(string input)
        {
            if (input == null)
                return null;

            if (input.IndexOf("qtd", StringComparison.OrdinalIgnoreCase) >= 0 || input.IndexOf("quarter", StringComparison.OrdinalIgnoreCase) >= 0)
                return "QTD";
            else if (input.IndexOf("mtd", StringComparison.OrdinalIgnoreCase) >= 0 || input.IndexOf("month", StringComparison.OrdinalIgnoreCase) >= 0)
                return "MTD";

            return null;
        }

        // TODO: Better matching algorithm
        private static string ClosestMatch(string[] matches, string input)
        {
            if (input == null)
                return null;

            foreach (var match in matches)
            {
                if (match.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0 || input.IndexOf(match, StringComparison.OrdinalIgnoreCase) >= 0)
                    return match;
            }

            return null;
        }
    }

    [Serializable]
    public class QueryForm
    {
        public string ExpenseCategory { get; set; }
        public string TimePeriod { get; set; }

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
    }
}

/*

MIT License

Copyright (c) 2016 Tess

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/
