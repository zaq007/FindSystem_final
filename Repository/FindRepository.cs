using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Data.DTO;
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

        public AnswerResult TryAnswer(int userId, string answer)
        {
            var user = _context.UserProfiles.Where(x => x.Id == userId).FirstOrDefault();
            if (user.EndTime != null)
            {
                throw new Exception("You have already ended the game");
            }
            if (user.State.IsFrozen == true)
            {
                throw new Exception("You are frozen");
            }

            var task = GetCurrentTask(userId);
            if (String.Compare(task.Answer, answer, true) == 0)
            {
                if (user.State.Position == 8)
                {
                    user.EndTime = DateTime.Now;
                    _context.SaveChanges();
                    return new AnswerResult()
                    {
                        End = true,
                        IsRight = true
                    };
                }
                user.State.Position++;
                _context.SaveChanges();
                task = GetCurrentTask(userId);
                task.Answer = "";
                return new AnswerResult()
                {
                    IsRight = true,
                    Task = task
                };                
            }
            else
                throw new Exception("Wrong answer");
        }

        public GameState GetGameState(int userId)
        {
            var user = _context.UserProfiles.Where(x => x.Id == userId).FirstOrDefault();
            ITask task = GetCurrentTask(userId);
            if(task != null)
                task.Answer = "";

            return new GameState()
            {
                UserId = userId,
                Task = task,
                Scoreboard = Scoreboard(),
                IsFinished = user.EndTime.HasValue,
                IsStarted = user.StartTime.HasValue
            };
        }

        public IEnumerable<IScoreboard> Scoreboard()
        {
            return _context.Scoreboard.ToArray();
        }
    }
}
