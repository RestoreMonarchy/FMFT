using FMFT.Extensions.Blazor.Bases.Inputs;
using FMFT.Extensions.Blazor.Bases.Validations;
using Microsoft.AspNetCore.Components;
using System.Security;

namespace FMFT.Extensions.Blazor.Bases.Forms
{
    public partial class FormBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public string Class { get; set; }
        [Parameter]
        public EventCallback OnSubmit { get; set; }

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

        public void HandleErrors(Dictionary<string, string[]> errors)
        {
            foreach (string key in errors.Keys)
            {
                ValidationMessageBase validationMessage = ValidationMessages.FirstOrDefault(x => x.Key == key);
                if (validationMessage != null)
                {
                    string[] value = errors[key];
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
