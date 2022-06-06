using TranzactChallenge.DAL;

namespace TranzactChallenge.BLL
{
	public abstract class BLL<T, TRepository> where TRepository : XmlRepository<T>, new()
	{
		public TRepository Repository { get; } = new TRepository();
	}
}