using Microsoft.AspNetCore.Components;

namespace BlazorTestApp.Components.Components.Loading
{
	public partial class Loading : ComponentBase, IDisposable
	{
		[Inject] LoadingProvider _LoadingProvider { get; set; }

		protected bool IsVisible { get; set; }

		protected override void OnInitialized()
		{
			_LoadingProvider.OnShow += Show;
			_LoadingProvider.OnHide += Hide;
		}
		/// <summary>
		/// Show loading
		/// </summary>
		private void Show()
		{
			IsVisible = true;
			StateHasChanged();
		}

		/// <summary>
		/// Hide loading
		/// </summary>
		private void Hide()
		{
			IsVisible = false;
			StateHasChanged();
		}

		/// <summary>
		/// Hide loading
		/// </summary>
		public void Dispose()
		{
			Hide();
		}
	}
}
