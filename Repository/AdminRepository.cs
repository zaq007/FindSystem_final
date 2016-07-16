using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using DAL;
using Newtonsoft.Json.Linq;

namespace Repository
{
    public class AdminRepository
    {
        readonly EFContextProvider<FindContext> _contextProvider =
            new EFContextProvider<FindContext>();

        public string Metadata()
        {
            return _contextProvider.Metadata();
        }


        public SaveResult SaveChanges(JObject saveBundle)
        {
            return _contextProvider.SaveChanges(saveBundle);
        }

        public IQueryable UserProfiles { get { return _contextProvider.Context.UserProfiles; } }

        public IQueryable Pathes { get { return _contextProvider.Context.Pathes; } }

        public IQueryable Tasks { get { return _contextProvider.Context.Tasks; } }

        public IQueryable Path_Task { get { return _contextProvider.Context.Path_Task; } }

    }
}
