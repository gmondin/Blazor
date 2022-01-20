using BlazorTestApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BlazorTestApp.Shared.Helpers
{
	public static class ClassHelper
	{
		#region MethodToProperties

		public static string GetPropertyDisplayName<T>(Expression<Func<T, object>> propertyExpression)
		{
			var memberInfo = GetPropertyInformation(propertyExpression.Body);
			if (memberInfo == null)
			{
				return string.Empty;
			}

			var attr = memberInfo.GetAttribute<DisplayNameAttribute>(false);
			if (attr == null)
			{
				return memberInfo.Name;
			}

			return attr.DisplayName;
		}


		public static Dictionary<string, string> GetPropertiesDisplayName<T>()
		{
			Dictionary<string, string> properties = new();

			var classList = typeof(T).GetProperties().Distinct().ToList();

			foreach (PropertyInfo prop in classList)
			{
				string name = GetDisplayName(prop);
				if (!string.IsNullOrWhiteSpace(name)) properties.Add(prop.Name, name);
			}

			return properties;
		}

		public static string[] GetPropertiesName<T>()
		{
			var classList = typeof(T).GetProperties().Distinct().ToList();

			string[] properties = new string[classList.Count()];

			for (int i = 0; i < classList.Count(); i++)
			{
				properties[i] = classList[i].Name;
			}

			return properties;
		}

		public static string GetValueFromProperty<T>(T element, string propertyName)
		{
			var list = element.GetType().GetProperties();

			foreach (PropertyInfo prop in list)
			{
				object? value = prop.GetValue(element, null);
				if (value != null) return value.ToString();
			}

			return string.Empty;
		}

		public static string GetDisplayName(PropertyInfo property, bool getAttributeIfDisplayIsNull = false)
		{
			var attrName = GetAttributeDisplay(property);
			if (!string.IsNullOrEmpty(attrName))
				return attrName;

			var attrDisplayName = GetAttributeDisplayName(property);
			if (!string.IsNullOrEmpty(attrDisplayName))
				return attrDisplayName;

			var metaName = GetMetaDisplayName(property);
			if (!string.IsNullOrEmpty(metaName))
				return metaName;

			if (!getAttributeIfDisplayIsNull) return string.Empty;

			return property.Name.ToString();
		}

		#endregion

		#region MethodToConvertListToDataTable

		public static DataTable ToDataTable<T>(List<T> items)
		{
			DataTable dataTable = new DataTable(typeof(T).Name);
			PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo prop in Props)
			{
				dataTable.Columns.Add(prop.Name);
			}
			foreach (T item in items)
			{
				var values = new object?[Props.Length];
				for (int i = 0; i < Props.Length; i++)
				{
					values[i] = Props[i].GetValue(item, null);
				}
				dataTable.Rows.Add(values);
			}

			return dataTable;
		}

		#endregion

		#region MethodToCheckValue

		public static bool ContainValueElement<T>(T element, string searchString)
		{
			var list = element?.GetType().GetProperties();

			foreach (PropertyInfo prop in list)
			{
				if (prop.GetValue(element).ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)) return true;
			}

			return false;
		}
		#endregion

		#region MethodToCompare


		public static bool CompareElement<T>(T element, T elementToCompare)
		{
			if (elementToCompare == null || element == null) return false;

			var list = element.GetType().GetProperties();

			var listToCompare = elementToCompare.GetType().GetProperties();

			for (int i = 0; i < list.Count(); i++)
			{
				if (list[i].GetValue(element).ToString() != listToCompare[i].GetValue(element).ToString()) return false;
			}

			return true;
		}

		#endregion

		#region MethodToFilterList

		public static IEnumerable<T> ListFilterSearchableField<T>(List<T> listData, string searchString)
		{
			return listData.Where(element =>
			{
				if (string.IsNullOrWhiteSpace(searchString))
					return true;

				var list = element.GetType().GetProperties();

				foreach (PropertyInfo prop in list)
				{
					if (IsAttributeSearchable(prop) && prop.GetValue(element).ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)) return true;
				}
				return false;
			}).ToArray();
		}

		public static IEnumerable<T> ListFilterAnyField<T>(List<T> listData, string searchString)
		{
			return listData.Where(element =>
			{
				if (string.IsNullOrWhiteSpace(searchString))
					return true;

				var list = element.GetType().GetProperties();

				foreach (PropertyInfo prop in list)
				{
					if (prop.GetValue(element).ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)) return true;
				}

				return false;
			}).ToArray();
		}

		public static List<T> ListFilterPerField<T>(IEnumerable<T> listData, string searchString, string[] fields)
		{
			return listData.Where(element =>
			{
				if (string.IsNullOrWhiteSpace(searchString))
					return true;

				foreach (var item in fields)
				{
					if (element.GetType().GetProperty(item).GetValue(element).ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)) return true;
				}
				return false;
			}).ToList();
		}
		#endregion

		#region MethodToSelect

		public static bool IsAttributeSearchable(PropertyInfo property)
		{
			var atts = property.GetCustomAttributes(
					typeof(SearchtableAttribute), true);
			if (atts.Length == 0)
				return false;
			return (atts[0] as SearchtableAttribute).IsSearchtable;
		}

		#endregion

		#region MethodWsWarning

		public static bool CanFollow<T>(T element)
		{
			var list = element.GetType().GetProperties();

			var error = list.Where(x => x.GetType() == typeof(CustomException)).FirstOrDefault();

			if (error != null)
			{
				return false;
			}
			else
			{
				var warning = list.Where(x => x.GetType() == typeof(List<CustomWarning>)).FirstOrDefault();

				if (warning == null) warning = list.Where(x => x.GetType() == typeof(CustomWarning)).FirstOrDefault();
			}
			return true;
		}

		#endregion

		#region MethodHelpers

		private static T GetAttribute<T>(this MemberInfo member, bool isRequired)
	 where T : Attribute
		{
			var attribute = member.GetCustomAttributes(typeof(T), false).SingleOrDefault();

			if (attribute == null && isRequired)
			{
				throw new ArgumentException(
						string.Format(
								CultureInfo.InvariantCulture,
								"The {0} attribute must be defined on member {1}",
								typeof(T).Name,
								member.Name));
			}

			return (T)attribute;
		}

		private static MemberInfo GetPropertyInformation(Expression propertyExpression)
		{
			MemberExpression memberExpr = propertyExpression as MemberExpression;
			if (memberExpr == null)
			{
				UnaryExpression unaryExpr = propertyExpression as UnaryExpression;
				if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
				{
					memberExpr = unaryExpr.Operand as MemberExpression;
				}
			}

			if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
			{
				return memberExpr.Member;
			}

			return null;
		}


		private static string GetAttributeDisplayName(PropertyInfo property)
		{
			var atts = property.GetCustomAttributes(
					typeof(DisplayNameAttribute), true);
			if (atts.Length == 0)
				return null;
			return (atts[0] as DisplayNameAttribute).DisplayName;
		}

		private static string GetAttributeDisplay(PropertyInfo property)
		{
			var atts = property.GetCustomAttributes(
					typeof(DisplayAttribute), true);
			if (atts.Length == 0)
				return null;
			return (atts[0] as DisplayAttribute).Name;
		}

		private static string GetMetaDisplayName(PropertyInfo property)
		{
			var atts = property.DeclaringType.GetCustomAttributes(
					typeof(MetadataTypeAttribute), true);
			if (atts.Length == 0)
				return null;

			var metaAttr = atts[0] as MetadataTypeAttribute;
			var metaProperty =
					metaAttr.MetadataClassType.GetProperty(property.Name);
			if (metaProperty == null)
				return null;
			return GetAttributeDisplayName(metaProperty);
		}

		#endregion
	}
}
