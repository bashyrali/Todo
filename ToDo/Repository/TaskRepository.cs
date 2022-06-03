using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Repository
{
    public class TaskRepository : IRepositoryTask
    {
        private readonly StoreContext _storeContext;

        public TaskRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetAllById(int projectId)
        {
            return await _storeContext.Tasks.Where(t => t.ProjectId == projectId).ToListAsync();
        }
        

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetAll()
        {
            return await _storeContext.Tasks.ToListAsync();
        }

        public async System.Threading.Tasks.Task<Task> Get(int id)
        {
            var result = await _storeContext.Tasks.Include(t => t.Project).FirstOrDefaultAsync(p => p.Id == id);
            return result;
        }

        public async System.Threading.Tasks.Task<Task> Create(Task item)
        {
            if (item.Project != null)
            {
                 _storeContext.Entry(item.Project).State = EntityState.Unchanged;
             }
            var project = await _storeContext.Tasks.AddAsync(item);
            await _storeContext.SaveChangesAsync();
            return project.Entity;
        }

        public async System.Threading.Tasks.Task<Task> Update(Task item)
        {
            var result = await _storeContext.Tasks.FirstOrDefaultAsync(p => p.Id == item.Id);
            if (result != null)
            {
                result.Name = item.Name;
                result.Priority = item.Priority;
                result.Status = item.Status;
                result.Description = item.Description;
                result.ProjectId = item.ProjectId;
                await _storeContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
        

        public async System.Threading.Tasks.Task Delete(int id)
        {
            
            var result = await _storeContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (result != null)
            {
                _storeContext.Projects.Remove(result);
                await _storeContext.SaveChangesAsync();
            }
        }
    }
}