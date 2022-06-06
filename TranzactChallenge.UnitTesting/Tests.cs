using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TranzactChallenge.BLL;
using TranzactChallenge.Entities;

namespace TranzactChallenge.UnitTesting
{
	public sealed class Tests
	{
		[Test]
		public void ExampleTest()
		{
			InsuranceBLL bll = new InsuranceBLL();
			Insurance.CalculatorRequest request = new Insurance.CalculatorRequest()
			{
				Plan = new Plan() { Name = "C" },
				State = new State() { Code = "NY" },
				MonthOfBirth = Month.December,
				Age = 50,
				Period = 1
			};
			IEnumerable<Insurance> insurances = bll.ReadByCalculatorRequest(request);
			if (insurances.Count() >= 2) Assert.Pass();
			else Assert.Fail(message: "Premium Calculator must return at least 2 insurancy plans.");
		}
	}
}