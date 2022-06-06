using System.Collections.Generic;
using System.Linq;
using TranzactChallenge.DAL;
using TranzactChallenge.Entities;
using static TranzactChallenge.Entities.Insurance;

namespace TranzactChallenge.BLL
{
	public sealed class InsuranceBLL : BLL<Insurance, InsuranceRepository>
	{
		public IEnumerable<Insurance> ReadAll() => Repository.SelectAll();
		public IEnumerable<Insurance> ReadByCalculatorRequest(CalculatorRequest request)
		{
			Plan plan = request.Plan;
			Insurance[] result = ReadAll().Where(predicate: i => i.Plans.Any(predicate: ii => ii.Name == plan.Name)).ToArray();
			if (result.Length > 0)
			{
				byte age = request.Age;
				result = result.Where(predicate: i => i.AgeRangeFrom <= age && i.AgeRangeTo >= age).ToArray();
				if (result.Length > 0)
				{
					State state = request.State;
					result = result.Where(predicate: i => i.State.Code == state.Code || i.State == State.Wildcard).ToArray();
					if (result.Length > 0)
					{
						Month monthOfBirth = request.MonthOfBirth ?? default;
						result = result.Where(predicate: i => i.MonthOfBirth == monthOfBirth || i.MonthOfBirth == Month.Wildcard).ToArray();
						if (result.Length > 0)
						{
							result = result.GroupBy(keySelector: i => i.Carrier).SelectMany(selector: i =>
							{
								Insurance match = i.OrderByDescending(keySelector: ii => ii.SpecificityLevel).FirstOrDefault();
								return match != default ? new Insurance[] { match } : Enumerable.Empty<Insurance>();
							}).ToArray();
						}
					}
				}
			}
			return result;
		}
		public IEnumerable<Plan> ReadPlans() => ReadAll().SelectMany(selector: i => i.Plans).GroupBy(keySelector: i => i.Name).Select(selector: i => i.First());
	}
}