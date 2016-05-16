using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using Breeze.WebApi2;
using FindSystem.Models;
using Newtonsoft.Json.Linq;

namespace FindSystem.Controllers
{
    [BreezeController]
    public class EntityController : ApiController
    {

        readonly EFContextProvider<UsersContext> _contextProvider =
                new EFContextProvider<UsersContext>();

        // ~/breeze/todos/Metadata 
        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }

        // ~/breeze/todos/SaveChanges
        //[HttpPost]
        //public SaveResult SaveChanges(JObject saveBundle)
        //{
        //    return _contextProvider.SaveChanges(saveBundle);
        //
        //}
        //
        //[HttpGet]
        //[EnableBreezeQuery]
        //public Task<IHttpActionResult> State()
        //{
        //    return System.Threading.Tasks.Task.FromResult<IHttpActionResult>(Ok(_contextProvider.Context.States.Where(x => x.Team.UserId == WebMatrix.WebData.WebSecurity.CurrentUserId)));
        //}
    }
}
