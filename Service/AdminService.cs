using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breeze.ContextProvider;
using Newtonsoft.Json.Linq;
using Repository;

namespace Service
{
    public class AdminService
    {
        readonly AdminRepository _repository = new AdminRepository();

        public string Metadata()
        {
            return _repository.Metadata();
        }


        public SaveResult SaveChanges(JObject saveBundle)
        {
            return _repository.SaveChanges(saveBundle);
        }

        public IQueryable UserProfiles { get { return _repository.UserProfiles; } }

        public IQueryable Tasks { get { return _repository.Tasks; } }

        public IQueryable Pathes { get { return _repository.Pathes; } }

        public IQueryable Path_Task { get { return _repository.Path_Task; } }

    }
}
