using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Data.Entity;

namespace Repository
{
    public class FindRepository
    {
        readonly FindContext _context = new FindContext();

        public ITask GetCurrentTask(int userId)
        {
            return _context.GetCurrentTask(userId).FirstOrDefault(); 
        }
        
        public ITask Start(int userId)
        {
            var user = _context.UserProfiles.Where(x => x.Id == userId).FirstOrDefault();
            if (user.StartTime == null)
            {
                user.StartTime = DateTime.Now;
                user.State.Position = 1;
                _context.SaveChanges();
                ITask task = GetCurrentTask(userId);
                task.Answer = "";
                return task;
            }
            throw new Exception("Have already started");
        }
    }
}
