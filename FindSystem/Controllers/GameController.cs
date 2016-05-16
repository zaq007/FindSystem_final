using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using Breeze.ContextProvider.EF6;
using FindSystem.Hubs;
using FindSystem.Models;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using WebMatrix.WebData;

namespace FindSystem.Controllers
{
    
    [System.Web.Mvc.Authorize]
    public class GameController : Controller
    {
        readonly UsersContext _contextProvider = new UsersContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Start()
        {
            var user = _contextProvider.UserProfiles.Where(x => x.UserId == WebMatrix.WebData.WebSecurity.CurrentUserId).FirstOrDefault();
            if (user.StartTime == null)
            {
                user.StartTime = DateTime.Now;
                user.State.Position = 1;
                _contextProvider.SaveChanges();
                return Json(_contextProvider.Tasks.Where(x => x.Path.Team.UserId == WebMatrix.WebData.WebSecurity.CurrentUserId && x.Position == user.State.Position).Select(x => new { img = x.Img, comments = x.Comments, number = x.Position }));
            }
            return Json(new { message = "Have already started" });
        }

        [HttpPost]
        public ActionResult TryAnswer(string answer)
        {
            var user = _contextProvider.UserProfiles.Where(x => x.UserId == WebMatrix.WebData.WebSecurity.CurrentUserId).FirstOrDefault();
            if (user.EndTime != null)
            {
                return Json(new { message = "You have already ended the game" });
            }
            if (user.State.IsFrozen == true)
            {
                return Json(new { right = false, message = "You are frozen" });
            }
            var task = _contextProvider.Tasks.Where(x => x.Path.Team.UserId == WebMatrix.WebData.WebSecurity.CurrentUserId && x.Position == user.State.Position).FirstOrDefault();
            if (String.Compare(task.Answer, answer, true) == 0)
            {
                if (task.Position == 10)
                {
                    user.EndTime = DateTime.Now;
                    _contextProvider.SaveChanges();
                    return Json(new { end = true, right = true });
                }
                user.State.Position++;
                GlobalHost.ConnectionManager.GetHubContext<FindHub>().Clients.All.scoreboard(user.UserId);
                _contextProvider.SaveChanges();
                return Json(_contextProvider.Tasks.Where(x => x.Path.Team.UserId == WebMatrix.WebData.WebSecurity.CurrentUserId && x.Position == user.State.Position).Select(x => new { right = true, img = x.Img, comments = x.Comments, number = x.Position }).FirstOrDefault());
            } else
                return Json(new { right = false, end = false, message = "Wrong answer" });
        }

        public class ScoreboardModel
        {
            public string teamName { get; set; }
            public int teamId { get; set; }
            public int? position { get; set; }
        }

        [HttpGet]
        public ActionResult State()
        {
            var user = _contextProvider.UserProfiles.Where(x => x.UserId == WebMatrix.WebData.WebSecurity.CurrentUserId).FirstOrDefault();
            var score_arr = _contextProvider.States.Select(x => new ScoreboardModel { teamName = x.Team.Path.Name, teamId = x.Team.UserId, position = x.Position }).ToList<ScoreboardModel>();
            StringBuilder score_sb = new StringBuilder();
            JsonSerializer.Create().Serialize(new StringWriter(score_sb), score_arr);
            string score = score_sb.ToString();
            if (user.EndTime != null)
            {
                return Json(new { scoreboard = score, 
                    teamId = WebMatrix.WebData.WebSecurity.CurrentUserId, 
                    points = user.Points,
                    end = true
                });
            }
            if (user.StartTime == null)
            {
                return Json(new { scoreboard = score, 
                    teamId = WebMatrix.WebData.WebSecurity.CurrentUserId, 
                    points = user.Points }, JsonRequestBehavior.AllowGet);
            }
            return Json(_contextProvider.
                Tasks.
                Where(x => x.Path.Team.UserId == WebMatrix.WebData.WebSecurity.CurrentUserId 
                    && x.Position == user.State.Position).
                    Select(x => new { gameStarted = true, 
                        img = x.Img, 
                        comments = x.Comments, 
                        number = x.Position, 
                        scoreboard = score, 
                        teamId = WebMatrix.WebData.WebSecurity.CurrentUserId,
                        points = user.Points
                    }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Freeze(int userId)
        {
            var user = _contextProvider.UserProfiles.Where(x => x.UserId == userId).FirstOrDefault();
            var currentUser = _contextProvider.UserProfiles.Where(x => x.UserId == WebSecurity.CurrentUserId).FirstOrDefault();
            if (user.State.IsFrozen == true)
            {
                return Json(new { data = "User has already frozen" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (currentUser.Points < 2)
                {
                    return Json(new { data = "You have no enough points to freeze this team" }, JsonRequestBehavior.AllowGet);
                }
                user.State.IsFrozen = true;
                currentUser.Points -= 2;
                _contextProvider.SaveChanges();
                try
                {
                    GlobalHost.ConnectionManager.GetHubContext<FindHub>().Clients.Client(FindHub.clients[userId]).setFrozen();
                }
                catch
                {
                }
                Timer timer = new Timer(100000);
                timer.Elapsed += delegate
                {
                    user.State.IsFrozen = false;
                    _contextProvider.SaveChanges();
                    try
                    {
                        GlobalHost.ConnectionManager.GetHubContext<FindHub>().Clients.Client(FindHub.clients[userId]).setUnfrozen();
                    }
                    catch
                    {
                    }
                };
                timer.Start();
                return Json(new { data = "OK" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult Scoreboard()
        {
            return Json(_contextProvider.States.Select(x => new { teamName = x.Team.Path.Name, teamId = x.Team.UserId, position = x.Position }), JsonRequestBehavior.AllowGet);
        }



    }
}
