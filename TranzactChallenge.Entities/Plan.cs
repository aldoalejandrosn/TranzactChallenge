using System;
using System.Collections.Generic;

namespace TranzactChallenge.Entities
{
	public sealed class Plan : IEqualityComparer<Plan>
	{
		#region Methods
		public bool Equals(Plan x, Plan y) => x?.Name == y?.Name;
		public int GetHashCode(Plan obj) => obj?.Name?.GetHashCode() ?? throw new ArgumentNullException(paramName: nameof(obj));
		#endregion

		#region Properties
		public string Name { get; set; }
		#endregion
	}
	public static class PlanUtils
	{
		public static IEnumerable<Plan> ParseMultiple(string value) => value != default ? ParseMultipleInternal(value) : default;
		private static IEnumerable<Plan> ParseMultipleInternal(string value)
		{
			value = value.Trim();
			string[] values = value.Split(separator: ',');
			foreach (string item in values)
				yield return new Plan() { Name = item.Trim() };
		}
	}
}