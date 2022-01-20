using Microsoft.AspNetCore.Components;

namespace BlazorTestApp.Components.Components.Toast
{
	public partial class Toast : ComponentBase, IDisposable
	{
		[Inject] ToastProvider ToastService { get; set; }
		[Parameter] public ToastPosition Position { get; set; } = ToastPosition.CenterCenter;
		[Parameter] public int Timeout { get; set; }
		private string PositionClass { get; set; } = string.Empty;

		internal List<ToastInstance> ToastList { get; set; } = new List<ToastInstance>();

		protected override void OnInitialized()
		{
			ToastService.OnShow += ShowToast;
			SetToastPosition();
		}
		/// <summary>
		/// remove toest based on guid
		/// </summary>
		/// <param name="toastId"></param>
		public void RemoveToast(Guid toastId)
		{
			InvokeAsync(() =>
			{
				var toastInstance = ToastList.SingleOrDefault(x => x.Id == toastId);
				ToastList.Remove(toastInstance);
				StateHasChanged();
			});
		}
		/// <summary>
		/// remove all toasts from the list
		/// </summary>
		public void RemoveAllToasts()
		{
			ToastList.RemoveAt(ToastList.Count);
		}
		/// <summary>
		/// creating toast configuration
		/// </summary>
		/// <param name="level"></param>
		/// <param name="message"></param>
		/// <param name="heading"></param>
		/// <param name="sound"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		private ToastSettings BuildToastSettings(ToastLevel level, string message, string heading, string sound)
		{
			switch (level)
			{
				case ToastLevel.Info:
					return new ToastSettings(heading, message, "bg-info", "info-circle", string.IsNullOrWhiteSpace(sound) ? "info.mp3" : sound);
				case ToastLevel.Success:
					return new ToastSettings(heading, message, "bg-success", "check-circle", string.IsNullOrWhiteSpace(sound) ? "success.mp3" : sound);
				case ToastLevel.Warning:
					return new ToastSettings(heading, message, "bg-warning", "exclamation-circle", string.IsNullOrWhiteSpace(sound) ? "warning.mp3" : sound);
				case ToastLevel.Error:
					return new ToastSettings(heading, message, "bg-danger", "exclamation-triangle", string.IsNullOrWhiteSpace(sound) ? "error.mp3" : sound);
			}

			throw new InvalidOperationException();
		}
		/// <summary>
		/// position of toast on the screen
		/// </summary>
		private void SetToastPosition()
		{
			switch (Position)
			{
				case ToastPosition.TopLeft:
					PositionClass = "topleft";
					break;
				case ToastPosition.TopRight:
					PositionClass = "topright";
					break;
				case ToastPosition.TopCenter:
					PositionClass = "topcenter";
					break;
				case ToastPosition.CenterLeft:
					PositionClass = "centerleft";
					break;
				case ToastPosition.CenterRight:
					PositionClass = "centerright";
					break;
				case ToastPosition.CenterCenter:
					PositionClass = "centercenter";
					break;
				case ToastPosition.BottomLeft:
					PositionClass = "bottomleft";
					break;
				case ToastPosition.BottomRight:
					PositionClass = "bottomright";
					break;
				case ToastPosition.BottomCenter:
					PositionClass = "bottomcenter";
					break;
			}
		}
		/// <summary>
		/// show toast
		/// </summary>
		/// <param name="level"></param>
		/// <param name="message"></param>
		/// <param name="heading"></param>
		/// <param name="autoHide"></param>
		/// <param name="sound"></param>
		/// <param name="timeOut"></param>
		private void ShowToast(ToastLevel level, string message, string heading, bool autoHide, string sound, int timeOut)
		{
			InvokeAsync(() =>
			{
				var settings = BuildToastSettings(level, message, heading, sound);
				var toast = new ToastInstance
				{
					Id = Guid.NewGuid(),
					TimeStamp = DateTime.Now,
					ToastSettings = settings
				};

				ToastList.Add(toast);

				if (autoHide)
				{
					var timeOutMilliSecondi = timeOut == 0 ? Timeout * 1000 : timeOut * 1000;
					var toastTimer = new System.Timers.Timer(timeOutMilliSecondi);
					toastTimer.Elapsed += (sender, args) => { RemoveToast(toast.Id); };
					toastTimer.AutoReset = false;
					toastTimer.Start();
				}
				StateHasChanged();
			});
		}

		public void Dispose()
		{
			ToastService.OnShow -= ShowToast;
		}
	}
}
