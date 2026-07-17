using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Exceptions
{
    internal class TaskValidationException : Exception
    {
        public TaskValidationException(string message) : base(message)
        { 

        }
    }
}
