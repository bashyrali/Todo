using System.Collections.Generic;
using System.Threading.Tasks;
using Task = ToDo.Models.Task;

namespace ToDo.Repository
{
    public interface IRepositoryTask
    {
        Task<IEnumerable<Task>> GetAllById(int projectId);
        Task<IEnumerable<Task>> GetAll();
        Task<Task> Get(int id);
        
        Task<Task> Create(Task item);
        Task<Task> Update(Task item);
        System.Threading.Tasks.Task Delete(int id);
    }
}