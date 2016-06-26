using System;
using System.Web.Mvc;
using Data.DTO;
using FindSystem.Hubs;
using Microsoft.AspNet.SignalR;
using Service;
using WebMatrix.WebData;

namespace FindSystem.Controllers
{

    [System.Web.Mvc.Authorize]
    public class GameController : Controller
    {
        readonly FindService _service = new FindService();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Start()
        {
            try
            {
                return Json(_service.Start(WebSecurity.CurrentUserId));
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult TryAnswer(string answer)
        {
            try
            {
                AnswerResult result = _service.TryAnswer(WebSecurity.CurrentUserId, answer);
                if(result.End)
                    GlobalHost.ConnectionManager.GetHubContext<FindHub>().Clients.All.userFinished(WebSecurity.CurrentUserId);
                else
                    GlobalHost.ConnectionManager.GetHubContext<FindHub>().Clients.All.userAnswered(WebSecurity.CurrentUserId);
                return Json(result);
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message });
            }
        }
        
        [HttpGet]
        public ActionResult State()
        {
            try
            {
                return Json(_service.GetGameState(WebSecurity.CurrentUserId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message }, JsonRequestBehavior.AllowGet);
            }        
        }
        
        //[HttpGet]
        //public ActionResult Freeze(int userId)
        //{
        //    var user = _contextProvider.UserProfiles.Where(x => x.UserId == userId).FirstOrDefault();
        //    var currentUser = _contextProvider.UserProfiles.Where(x => x.UserId == WebSecurity.CurrentUserId).FirstOrDefault();
        //    if (user.State.IsFrozen == true)
        //    {
        //        return Json(new { data = "User has already frozen" }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        if (currentUser.Points < 1)
        //        {
        //            return Json(new { data = "You have no enough points to freeze this team" }, JsonRequestBehavior.AllowGet);
        //        }
        //        user.State.IsFrozen = true;
        //        currentUser.Points -= 1;
        //        _contextProvider.SaveChanges();
        //        try
        //        {
        //            GlobalHost.ConnectionManager.GetHubContext<FindHub>().Clients.Client(FindHub.clients[userId]).setFrozen();
        //        }
        //        catch
        //        {
        //        }
        //        Timer timer = new Timer(300000);
        //        timer.Elapsed += delegate
        //        {
        //            user.State.IsFrozen = false;
        //            _contextProvider.SaveChanges();
        //            try
        //            {
        //                GlobalHost.ConnectionManager.GetHubContext<FindHub>().Clients.Client(FindHub.clients[userId]).setUnfrozen();
        //            }
        //            catch
        //            {
        //            }
        //        };
        //        timer.Start();
        //        return Json(new { data = "OK" }, JsonRequestBehavior.AllowGet);
        //    }
        //
        //}

        [HttpGet]
        public ActionResult Scoreboard()
        {
            return Json(_service.Scoreboard(), JsonRequestBehavior.AllowGet);
        }
    }
}
