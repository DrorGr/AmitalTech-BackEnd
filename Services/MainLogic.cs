using AmitalBE.Context;
using AmitalBE.Models;
using AmitalBE.Request;
using AmitalBE.Response;
using Task = AmitalBE.Models.Task;

namespace AmitalBE.Services
{
    public class MainLogic
    {

        private readonly MainDBContext _dbContext;

        public MainLogic ( MainDBContext dbContext )
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetUsers ( )
        {
            return _dbContext.Users;
        }

        public User GetUser ( int id )
        {
            return _dbContext.Users.Single(u => u.Id == id);
        }

        public IEnumerable<Task> GetTasks ( int? id )
        {
            if (!id.HasValue)
            {
                return _dbContext.Tasks;
            }

            return _dbContext.Tasks.Where(u => u.UserId == id);

        }


        public Task GetTask ( int id )
        {
            return _dbContext.Tasks.Single(t => t.UserId == id);
        }


        public IEnumerable<Task> GetTomorowTasks ( )
        {
            DateTime tomorrow = DateTime.Today.AddDays(1);
            return _dbContext.Tasks.Where(t => t.TargetDate == tomorrow).ToList();
        }

        public TaskResponse AddTask ( NewTaskRequest value )
        {
            var response = new TaskResponse();

            if (_dbContext.Tasks.Any(t => t.UserId == value.UserId && t.Subject == value.Subject))
            {
                response.ErrorMessage = "User has a task with the same subject";
                return response;
            }

            if (!IsUserTasksCountLessThanTen(value.UserId))
            {
                response.ErrorMessage = "User reached task amount limit";
                return response;
            }

            try
            {
                var task = new Task
                {
                    Subject = value.Subject,
                    IsCompleted = value.IsCompleted,
                    TargetDate = value.TargetDate,
                    UserId = value.UserId
                };
                _dbContext.Tasks.Add(task);
                _dbContext.SaveChanges();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = $"An error occurred while adding the task: {ex.Message}";
            }
            return response;
        }


        public bool IsUserTasksCountLessThanTen ( int userId )
        {
            int taskCount = _dbContext.Tasks.Count(t => t.UserId == userId);

            return taskCount < 10;
        }
    }
}
