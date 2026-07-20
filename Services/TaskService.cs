using System;
using System.Collections.Generic;
using System.Text;
using Task = TaskManager.Models.Task;
using TaskManager.Exceptions;
using TaskManager.Enum;

namespace TaskManager.Services
{
    internal class TaskService
    {
        private readonly List<Task>
            tasks = new List<Task>();
        public void AddTask(Task task)
        {
            if (string.IsNullOrWhiteSpace(task.Title) || task.Title.Length < 3 || task.Title.Length > 100)
            {
                throw new TaskValidationException("Hata: Başlık boş olamaz ve 3 ile 100 karakter arasında olmalıdır.");
            }
            if (task.DueDate != null && task.DueDate.Value < DateTime.Now)
            {
                throw new TaskValidationException("Hata: Bitiş tarihi geçmiş bir tarih olamaz.");
            }
            
            tasks.Add(task);
        }
        public IEnumerable<Task> GetAllTasks()
        {
            return tasks;
        }
        public Task? GetTaskById(Guid id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }
        public void UpdateTask(Guid id, Task updatedTask)
        {
            var existingTask = GetTaskById(id);
            if (GetTaskById(id) == null)
            {
                throw new TaskValidationException($"Hata: {id} id'li görev bulunamadı.");
            }
            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.Priority = updatedTask.Priority;
            existingTask.Status = updatedTask.Status;
            existingTask.DueDate = updatedTask.DueDate;
        }
        public void CompletedTask(Guid id)
		{
			var task = GetTaskById(id);
			if (task == null)
			{
				throw new TaskValidationException($"Hata: {id} id'li görev bulunamadı.");
			}

            if (task.Status == Status.Completed) {
				throw new TaskValidationException($"Hata: {id} id'li görev zaten tamamlanmış.");
			}
			task.Status = Status.Completed;
			task.CompletedAt = DateTime.Now;
		}
		public void DeleteTask(Guid id)
        {
            var taskToDelete = GetTaskById(id);
            if (taskToDelete == null)
            {
                throw new TaskValidationException($"Hata: {id} id'li görev bulunamadı.");
            }
            tasks.Remove(taskToDelete);
        }
        public IEnumerable<Task> GetTasksByStatus(Status status)
        {
			return tasks.Where(t => t.Status == status);
            
        }
        public IEnumerable<Task> GetTasksByPriority(Priority priority)
        {
			return tasks.Where(t => t.Priority == priority);
            
        }
        public IEnumerable<Task> SearchTasks(string keyword)
		{
            if (string.IsNullOrWhiteSpace(keyword))
                {
                    return tasks;
                }
			return tasks.Where(t => t.Title.ToLower().Contains(keyword.ToLower()) || t.Description != null && t.Description.ToLower().Contains(keyword.ToLower()));

		}
}}
