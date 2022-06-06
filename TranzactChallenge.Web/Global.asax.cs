using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TranzactChallenge.Web
{
	public abstract class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfiguration.RegisterRoutes(routes: RouteTable.Routes);
		}
	}
}