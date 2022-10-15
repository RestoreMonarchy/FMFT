using FMFT.Web.Client.Views.Bases.Buttons;
using FMFT.Web.Client.Views.Bases.Inputs;
using FMFT.Web.Client.Views.Bases.Validations;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using Xeptions;

namespace FMFT.Web.Client.Views.Bases.Forms
{
    public partial class FormBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public List<InputBase> Inputs { get; set; } = new List<InputBase>();
        public List<ValidationMessageBase> ValidationMessages { get; set; } = new List<ValidationMessageBase>();

        public void AddInput(InputBase inputBase)
        {
            Inputs.Add(inputBase);
        }

        public void AddValidationMessage(ValidationMessageBase validationMessage)
        {
            ValidationMessages.Add(validationMessage);
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

        public void HandleValidationXeption(Xeption xeption)
        {
            foreach (string key in xeption.Data.Keys)
            {
                ValidationMessageBase validationMessage = ValidationMessages.FirstOrDefault(x => x.Key == key);
                if (validationMessage != null)
                {
                    string[] value = xeption.Data[key] as string[];
                    validationMessage.Enable(value);
                }                
            }
        }

        public void ClearValidations()
        {
            foreach (ValidationMessageBase validationMessage in ValidationMessages)
            {
                validationMessage.Disable();
            }
        }

    }
}
