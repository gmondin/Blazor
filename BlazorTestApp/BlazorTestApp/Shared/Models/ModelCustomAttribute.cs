using BlazorTestApp.Shared.Helpers;
using BlazorTestApp.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace BlazorTestApp.Shared.Models
{
	public class ModelCustomAttribute
	{
		[Display(Name = "Name", ResourceType = typeof(Resource))]
		[SearchtableAttribute(IsSearchtable = true)]
		public string? Name { get; set; }	
	}
}
