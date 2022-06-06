using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TranzactChallenge.Entities
{
	public sealed class Insurance
	{
		#region Classes
		public sealed class CalculatorRequest
		{
			[Required(ErrorMessage = "Plan cannot be empty.")]
			public Plan Plan { get; set; }
			[Required(ErrorMessage = "State cannot be empty.")]
			public State State { get; set; }
			[Required(ErrorMessage = "Month of birth cannot be empty.")]
			public Month? MonthOfBirth { get; set; }
			[Required(ErrorMessage = "Age cannot be empty.")]
			public byte Age { get; set; }
			[Required(ErrorMessage = "Period cannot be empty.")]
			public byte Period { get; set; }
		}
		#endregion

		#region Properties - Basic
		public string Carrier { get; set; }
		public IEnumerable<Plan> Plans { get; set; }
		public State State { get; set; }
		public Month MonthOfBirth { get; set; }
		public byte AgeRangeFrom { get; set; }
		public byte AgeRangeTo { get; set; }
		public decimal Premium { get; set; }
		#endregion

		#region Properties - Extension
		public byte SpecificityLevel => (byte)((State != State.Wildcard ? 2 : 0) + (MonthOfBirth != Month.Wildcard ? 1 : 0));
		#endregion
	}
}