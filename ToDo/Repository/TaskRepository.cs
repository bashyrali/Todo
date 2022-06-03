using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Repository
{
    public class TaskRepository : IRepository<Task>
    {
        private readonly StoreContext _storeContext;

        public TaskRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetAll()
        {
            return await _storeContext.Tasks.ToListAsync();
        }

        public async System.Threading.Tasks.Task<Task> Get(int id)
        {
            var result =  await _storeContext.Tasks.Include(t => t.Project).FirstOrDefaultAsync(p => p.Id == id);
            return result;
        }

        public async System.Threading.Tasks.Task<Task> Create(Task item)
        {
            var project = await _storeContext.Tasks.AddAsync(item);
            await _storeContext.SaveChangesAsync();
            return project.Entity;
        }

        public System.Threading.Tasks.Task<Task> Update(Task item)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}