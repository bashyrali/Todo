using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Models;
using Task = System.Threading.Tasks.Task;

namespace ToDo.Repository
{
    public interface IRepositoryProject
    {
        Task<IEnumerable<Project>> GetAll();
        Task<Project> Get(int id);
        
        Task<Project> Create(Project item);
        Task<Project> Update(Project item);
        Task Delete(int id);
    }
}