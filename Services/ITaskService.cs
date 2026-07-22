using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Enum;
using Task = TaskManager.Models.Task;

namespace TaskManager.Services
{
    internal interface ITaskService
    {
        void AddTask(Task task);
		IEnumerable<Task> GetAllTasks();
		Task? GetTaskById(Guid id);
		void UpdateTask(Guid id, Task updatedTask);
		void CompletedTask(Guid id);
		void DeleteTask(Guid id);
		IEnumerable<Task> GetTasksByStatus(Status status);
		IEnumerable<Task> GetTasksByPriority(Priority priority);
		IEnumerable<Task> SearchTasks(string keyword);
	}
}
