using Microsoft.AspNetCore.Components;

namespace BlazorTestApp.Components.Components.Toast
{
	public partial class ToastContent : ComponentBase
	{
		[CascadingParameter] private Toast ToastsContainer { get; set; }
		[Parameter] public Guid ToastId { get; set; }
		[Parameter] public ToastSettings ToastSettings { get; set; }

		/// <summary>
		/// On close remove the last ToastID
		/// </summary>
		public void Close()
		{
			ToastsContainer.RemoveToast(ToastId);
		}
	}
}
