using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using TaskManager.Enum;

namespace TaskManager.Models
{
    internal class Task
    {
       public Guid Id;
        public string Title;
        public string Description;
        public Priority Priority;
		public Status Status;
        public DateTime? DueDate;
        public DateTime CreatedAt;
        public DateTime? CompletedAt;

        public Task()
        {
            Id = Guid.NewGuid();
			CreatedAt = DateTime.Now;
            Status = Status.Pending;
		}
        }

}
