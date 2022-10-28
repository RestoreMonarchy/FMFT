using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FMFT.Extensions.Blazor.Bases.Conditions
{
    public partial class Switch
    {
        [Parameter]
        public object Expression { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private List<SwitchCase> cases = new();
        private DefaultSwitchCase defaultCase = null;

        public void AddCase(SwitchCase switchCase)
        {
            cases.Add(switchCase);
            StateHasChanged();
            Console.WriteLine("hello from addCase");
        }

        public void SetDefaultCase(DefaultSwitchCase defaultSwitchCase)
        {
            defaultCase = defaultSwitchCase;
            StateHasChanged();
            Console.WriteLine("hello from defaultCase");
        }

        private SwitchCase GetCurrentCase()
        {
            foreach (SwitchCase switchCase in cases)
            {
                if (Expression.Equals(switchCase.Value))
                {
                    return switchCase;
                }
            }

            return null;
        }
    }
}
