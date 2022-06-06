using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TranzactChallenge.BLL;
using TranzactChallenge.Entities;
using static TranzactChallenge.Entities.Insurance;

namespace TranzactChallenge.Web.Controllers
{
	[RoutePrefix(prefix: "")]
	public sealed class MainController : Controller
	{
		#region Methods
		[HttpPost]
		[Route(template: "Calculator/Calculate")]
		public ActionResult CalculatePremium(CalculatorRequest request)
		{
			if (ModelState.IsValid)
			{
				IEnumerable<Insurance> insurances = InsuranceBLL.ReadByCalculatorRequest(request);
				byte period = request.Period;
				return Json(data: insurances.Select(selector: i => new { carrier = i.Carrier, premium = i.Premium * period }));
			}
			else return new HttpStatusCodeResult(statusCode: HttpStatusCode.BadRequest);
		}
		[HttpGet]
		[Route]
		public ActionResult Index()
		{
			ViewBag.Plans = InsuranceBLL.ReadPlans();
			ViewBag.States = StateBLL.ReadAll();
			return View();
		}
		#endregion

		#region Properties
		private InsuranceBLL InsuranceBLL { get { return new InsuranceBLL(); } }
		private StateBLL StateBLL { get { return new StateBLL(); } }
		#endregion
	}
}