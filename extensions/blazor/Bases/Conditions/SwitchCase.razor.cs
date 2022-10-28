using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMFT.Extensions.Blazor.Bases.Conditions
{
    public partial class SwitchCase
    {
        [Parameter]
        public object Value { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [CascadingParameter]
        public Switch Switch { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Switch.AddCase(this);
            }
        }
    }
}
