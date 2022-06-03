using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;
using Task = System.Threading.Tasks.Task;


namespace ToDo.Repository
{
    public class ProjectRepository : IRepositoryProject
    
    {
        private readonly StoreContext _storeContext;

        public ProjectRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _storeContext.Projects.ToListAsync();
        }

        public async Task<Project> Get(int id)
        {
           var result =  await _storeContext.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == id);
           return result;
        }
        

        public async Task<Project> Create(Project item)
        {
            // if (item.Tasks != null)
            // {
            //     _storeContext.Entry(item.Tasks).State = EntityState.Unchanged;
            // }
            var project = await _storeContext.Projects.AddAsync(item);
            await _storeContext.SaveChangesAsync();
            return project.Entity;
        }

        public async Task<Project> Update(Project item)
        {
            var result = await _storeContext.Projects.FirstOrDefaultAsync(p => p.Id == item.Id);
            if (result != null)
            {
                result.Name = item.Name;
                result.Priority = item.Priority;
                result.Status = item.Status;
                result.Tasks = item.Tasks;
                result.CompleteDateTime = item.StartDateTime;
                result.StartDateTime = item.CompleteDateTime;
                await _storeContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task Delete(int id)
        {
            var result = await _storeContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
            
            foreach (var task in _storeContext.Tasks.Where(t => t.ProjectId == result.Id))
            {
                _storeContext.Tasks.Remove(task);
            }
            if (result != null)
            {
                _storeContext.Projects.Remove(result);
                await _storeContext.SaveChangesAsync();
            }
        }
    }
}