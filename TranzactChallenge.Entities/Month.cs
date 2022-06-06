using System;

namespace TranzactChallenge.Entities
{
	public enum Month : byte
	{
		Wildcard = 0,
		January = 1,
		February = 2,
		March = 3,
		April = 4,
		May = 5,
		June = 6,
		July = 7,
		August = 8,
		September = 9,
		October = 10,
		November = 11,
		December = 12
	}
	public static class MonthUtils
	{
		public static Month Parse(string value)
		{
			value = value?.Trim();
			if (!string.IsNullOrEmpty(value))
			{
				if (value == Constants.Wildcard) return Month.Wildcard;
				else
				{
					if (Enum.TryParse(value, ignoreCase: true, out Month result))
						return result;
					else throw new ArgumentException(message: "Value is not a valid month.");
				}
			}
			else throw new ArgumentException(message: "Value cannot be empty or null.");
		}
		public static bool TryParse(string value, out Month month)
		{
			try
			{
				month = Parse(value);
				return true;
			}
			catch
			{
				month = default;
				return default;
			}
		}
	}
}