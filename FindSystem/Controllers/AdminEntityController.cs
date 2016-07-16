using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using Breeze.WebApi2;
using Newtonsoft.Json.Linq;
using Service;

namespace FindSystem.Controllers
{
    [Authorize(Roles="Admin")]
    [BreezeController]
    public class AdminEntityController : ApiController
    {
        readonly AdminService _service = new AdminService();

        [HttpGet]
        public string Metadata()
        {
            return _service.Metadata();
        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            return _service.SaveChanges(saveBundle);
        }

        [HttpGet]
        public IQueryable UserProfiles()
        {
            return _service.UserProfiles;
        }

        [HttpGet]
        public IQueryable Tasks()
        {
            return _service.Tasks;
        }

        [HttpGet]
        public IQueryable Path_Task()
        {
            return _service.Path_Task;
        }

        [HttpGet]
        public IQueryable Pathes()
        {
            return _service.Pathes;
        }
    }
}
