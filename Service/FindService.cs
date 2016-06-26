using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DTO;
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

        public AnswerResult TryAnswer(int userId, string answer)
        {
            return _repository.TryAnswer(userId, answer);
        }

        public IEnumerable<IScoreboard> Scoreboard()
        {
            return _repository.Scoreboard();
        }

        public GameState GetGameState(int userId)
        {
            return _repository.GetGameState(userId);
        }
    }
}
