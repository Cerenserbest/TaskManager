using System;
using System.Collections.Generic;
using System.Text;
using Task = TaskManager.Models.Task;

namespace TaskManager.Services
{
    internal class TaskService
    {
        private readonly List<Task>
            tasks = new List<Task>();
       public void AddTask(Task task) {
            if (string.IsNullOrWhiteSpace(task.Title) || task.Title.Length<3 || task.Title.Length > 100)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hata: Başlık 3 ile 100 karakter arasında olmalıdır.");
                Console.ResetColor();
				return;
            }
            if (task.DueDate != null && task.DueDate.Value < DateTime.Now ) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hata: Bitiş tarihi geçmiş bir tarih olamaz.");
                Console.ResetColor();
				return;
            }
            
			tasks.Add(task);}
		public IEnumerable<Task> GetAllTasks() {
            return tasks;
        }
        public Task? GetTaskById(Guid id) {
			return tasks.FirstOrDefault(t => t.Id == id);
		}
    }

}
