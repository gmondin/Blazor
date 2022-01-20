namespace BlazorTestApp.Components.Components.Toast
{
  public class ToastSettings
  {
    public ToastSettings(string heading, string message, string baseClass, string iconClass, string sound)
    {
      Heading = heading;
      Message = message;
      BaseClass = baseClass;
      IconClass = iconClass;
      Sound = sound;
    }
    public string Heading { get; set; }
    public string Message { get; set; }
    public string BaseClass { get; set; }
    public string IconClass { get; set; }
    public string Sound { get; set; }
  }
}
