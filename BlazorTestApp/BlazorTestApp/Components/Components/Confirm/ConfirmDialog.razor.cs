using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorTestApp.Components.Components.Confirm
{
	public partial class ConfirmDialog : ComponentBase, IResultDialog
	{
		[Parameter] public string Message { get; set; } = "";
		[Parameter] public EventCallback<DialogResult> CloseDialog { get; set; }

		/// <summary>
		/// return the dialog result
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		public async Task OnClose(DialogResult result)
		{
			await CloseDialog.InvokeAsync(result);
		}
	}
}

