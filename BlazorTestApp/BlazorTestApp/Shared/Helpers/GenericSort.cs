using BlazorTestApp.Shared.Enums;
using System.Linq.Expressions;

namespace BlazorTestApp.Shared.Helpers
{
	/// <summary>
	/// make a sort b
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class GenericSort<T>
	{
		public static IEnumerable<T> PISort(IEnumerable<T> source, SortByEnum sortBy, string sortDirection)
		{
			var param = Expression.Parameter(typeof(T), "item");

			var sortExpression = Expression.Lambda<Func<T, object>>
					(Expression.Convert(Expression.Property(param, sortBy.ToString().ToLower()), typeof(object)), param);

			return sortDirection.ToLower() switch
			{
				"asc" => source.AsQueryable<T>().OrderBy<T, object>(sortExpression),
				"desc" => source.AsQueryable<T>().OrderByDescending<T, object>(sortExpression),
				_ => source,
			};
		}
	}
}