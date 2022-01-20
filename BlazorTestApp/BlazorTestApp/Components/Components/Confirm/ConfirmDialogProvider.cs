using BootstrapBlazor.Components;

namespace BlazorTestApp.Components.Components.Confirm
{
	/// <summary>
	/// create a standard configuration for confirm dialog
	/// </summary>
	public class ConfirmDialogProvider : IDisposable
	{
		private readonly DialogService _DialogService;
		public ConfirmDialogProvider(DialogService dialogService)
		{
			_DialogService = dialogService;
		}

		/// <summary>
		/// show confirm Dialog
		/// </summary>
		/// <param name="message"></param>
		/// <param name="head"></param>
		/// <returns></returns>
		public async Task<DialogResult> ShowConfirm(string message, string head = "")
		{
			return await _DialogService.ShowModal<ConfirmDialog>(new ResultDialogOption()
			{
				IsCentered = true,
				ShowHeaderCloseButton = false,
				IsKeyboard = false,
				Title = head,
				ComponentParamters = new Dictionary<string, object>
				{
					[nameof(ConfirmDialog.Message)] = message
				},
				ButtonNoText = "No",
				ButtonYesText = "Yes"
			});
		}

		public void Dispose()
		{

		}
	}
}
