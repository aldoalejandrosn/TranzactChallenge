using System;

namespace TranzactChallenge.Entities
{
	public sealed class State
	{
		#region Fields
		public readonly static State Wildcard;
		#endregion

		#region Constructors
		static State()
		{
			Wildcard = new State()
			{
				Code = Constants.Wildcard,
				Name = Constants.Wildcard
			};
		}
		#endregion

		#region Methods
		public static State Build(string code)
		{
			if (!string.IsNullOrEmpty(value: code))
			{
				if (code == Constants.Wildcard) return Wildcard;
				else return new State() { Code = code };
			}
			else return default;
		}
		#endregion

		#region Properties
		public string Code { get; set; }
		public string Name { get; set; }
		#endregion
	}
}