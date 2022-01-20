namespace BlazorTestApp.Shared.Helpers
{
  /// <summary>
  /// my custom attribute
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class SearchtableAttribute : Attribute
  {
    public bool IsSearchtable { get; set; }

    public SearchtableAttribute()
    {
    }
  }
}
