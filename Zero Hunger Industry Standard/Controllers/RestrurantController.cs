using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger_Industry_Standard.Dtos;
using Zero_Hunger_Industry_Standard.Models;

namespace Zero_Hunger_Industry_Standard.Controllers
{
    public class RestrurantController : Controller
    {
        // GET: Restrurant
        public readonly IRestrurantService ress;        
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async  Task<ActionResult> GetRestrurantDetails(long RestrurantId) 
        {

            var data = await ress.GetRestrurantDetails(RestrurantId);

            return View(data);        
        }


        [HttpPost]
        public async Task<ActionResult> CreateUpdateDeleteRestrurant(RestrurantDto Restrurant)
        {

            var data = await ress.CreateUpdateDeleteRestruran(Restrurant);

            return View(data);
        }
        [HttpPost]
        public async Task<ActionResult> OpenCollectionRequestForm(RestrurantDto Restrurant)
        {
            //log For Collection Requests 
            var data = await ress.OpenCollectionRequestForm(Restrurant);

            return View(data);
        }
        [HttpGet]

        public async Task<ActionResult> GetCollectionRequest(DateTime ? Date)
        {
            
            var data = await ress.GetCollectionRequest(Date);

            return View(data);
        }
        [HttpPost]
        public async Task<ActionResult> TakeFood(RestrurantFoodDto EmployeeID)
        {

            var data = await ress.TakeFood(EmployeeID);

            return View(data);
        }
        [HttpPost]
        public async Task<ActionResult> CreateUser(UserDto user)
        {

            var data = await ress.CreateUser(user);

            return View(data);
        }
        [HttpGet]

        public async Task<ActionResult> ViewUsers(long? userId)
        {

            var data = await ress.ViewUsers(userId);

            return View(data);
        }

    }
}