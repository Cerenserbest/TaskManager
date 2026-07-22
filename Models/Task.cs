using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using TaskManager.Enum;

namespace TaskManager.Models
{
    internal class Task
    {
       public Guid Id { get; private set; } //encapsulation
        public string Title;
        public string Description;
        public Priority Priority;
		public Status Status;
        public DateTime? DueDate;
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt;

        public Task()
        {
            Id = Guid.NewGuid();
			CreatedAt = DateTime.Now;
            Status = Status.Pending;
		}
        }

}
