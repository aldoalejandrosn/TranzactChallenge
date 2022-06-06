using System.Web.Mvc;
using System.Web.Routing;

namespace TranzactChallenge.Web
{
	public static class RouteConfiguration
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapMvcAttributeRoutes();
		}
	}
}