namespace BlazorTestApp.Components.Components.Loading
{
	public class LoadingProvider : IDisposable
	{
		public event Action? OnShow;
		public event Action? OnHide;

		/// <summary>
		/// Show the loading on the screen
		/// </summary>
		public void Show()
		{
			OnShow?.Invoke();
		}

		/// <summary>
		/// hide loading
		/// </summary>
		public void Hide()
		{
			OnHide?.Invoke();
		}

		/// <summary>
		/// hide loading
		/// </summary>
		public void Dispose()
		{
			OnHide?.Invoke();
		}
	}
}
