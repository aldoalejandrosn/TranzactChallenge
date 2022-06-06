using System.Collections.Generic;
using TranzactChallenge.DAL;
using TranzactChallenge.Entities;

namespace TranzactChallenge.BLL
{
	public sealed class StateBLL : BLL<State, StateRepository>
	{
		public IEnumerable<State> ReadAll() => Repository.SelectAll();
	}
}