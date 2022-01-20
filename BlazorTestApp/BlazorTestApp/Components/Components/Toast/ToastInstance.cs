namespace BlazorTestApp.Components.Components.Toast
{
	/// <summary>
	/// Information for create a list of unique toast
	/// </summary>
	public class ToastInstance
	{
		public Guid Id { get; set; }
		public DateTime TimeStamp { get; set; }
		public ToastSettings ToastSettings { get; set; }
	}
}
