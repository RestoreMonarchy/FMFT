using FMFT.Extensions.Blazor.Bases.Javascript.Params;
using Microsoft.JSInterop;

namespace FMFT.Extensions.Blazor.Bases.Javascript
{
    public class JavascriptEvents
    {
        public static event Func<KeyPressParams, Task> OnKeyPress;

        [JSInvokable]
        public static async Task TriggerOnKeyPressAsync(KeyPressParams @params)
        {
            if (OnKeyPress != null)
            {
                await OnKeyPress.Invoke(@params);
            }
        }
    }
}
