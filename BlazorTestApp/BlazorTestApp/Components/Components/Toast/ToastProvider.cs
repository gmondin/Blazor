using System;

namespace BlazorTestApp.Components.Components.Toast
{
  /// <summary>
  /// provider for help show the toasts
  /// </summary>
  public class ToastProvider : IDisposable
  {
    public event Action<ToastLevel, string, string, bool, string, int> OnShow;

    /// <summary>
    /// Show error toast (red color
    /// </summary>
    /// <param name="message"></param>
    /// <param name="heading"></param>
    /// <param name="autoHide">default is false</param>
    public void ShowError(string message, string heading = "", bool autoHide = false)
    {
      ShowToast(message, heading, ToastLevel.Error, autoHide);
    }

    /// <summary>
    /// Show info toast, color blue
    /// </summary>
    /// <param name="message"></param>
    /// <param name="heading"></param>
    /// <param name="autoHide">default false</param>
    public void ShowInfo(string message, string heading = "", bool autoHide = false)
    {
      ShowToast(message, heading, ToastLevel.Info, autoHide);
    }

    /// <summary>
    /// show sucess toast, color green
    /// </summary>
    /// <param name="message"></param>
    /// <param name="heading"></param>
    /// <param name="autoHide">default true, after some seconds, hide the toast</param>
    public void ShowSuccess(string message, string heading = "", bool autoHide = true)
    {
      ShowToast(message, heading, ToastLevel.Success, autoHide);
    }

    /// <summary>
    /// Show warning toast, color yellow
    /// </summary>
    /// <param name="message"></param>
    /// <param name="heading"></param>
    /// <param name="autoHide">default true, after some seconds, hide the toast</param>
    public void ShowWarning(string message, string heading = "", bool autoHide = true)
    {
      ShowToast(message, heading, ToastLevel.Warning, autoHide);
    }
    /// <summary>
    /// if you want create your toast withou default settings
    /// </summary>
    /// <param name="message"></param>
    /// <param name="heading"></param>
    /// <param name="level">type</param>
    /// <param name="autoHide">default false</param>
    /// <param name="sound">defaut is empty</param>
    /// <param name="timeOut">default is 0</param>
    public void ShowToast(string message, string heading, ToastLevel level, bool autoHide = false, string sound = "", int timeOut = 0)
    {
      OnShow?.Invoke(level, message, heading, autoHide, sound, timeOut);
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }
}
