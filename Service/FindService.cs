using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;
using Repository;

namespace Service
{
    public class FindService
    {
        readonly FindRepository _repository = new FindRepository();

        public ITask Start(int userId)
        {
            return _repository.Start(userId);            
        }
    }
}
