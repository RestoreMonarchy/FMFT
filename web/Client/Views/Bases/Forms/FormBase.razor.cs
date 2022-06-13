using FMFT.Web.Client.Views.Bases.Buttons;
using FMFT.Web.Client.Views.Bases.Inputs;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Bases.Forms
{
    public partial class FormBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public List<InputBase> Inputs { get; set; } = new List<InputBase>();

        public void AddInput(InputBase inputBase)
        {
            Inputs.Add(inputBase);
        }

        public void DisableAll()
        {
            foreach (InputBase input in Inputs)
            {
                input.Disable();
            }
        }

        public void EnableAll()
        {
            foreach (InputBase input in Inputs)
            {
                input.Enable();
            }
        }
    }
}
