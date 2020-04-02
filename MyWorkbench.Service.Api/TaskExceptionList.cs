using System;
using System.Collections.Generic;

namespace MyWorkBench.Service.Api
{
    public class TaskExceptionList : Exception
    {
        public List<Exception> TaskExceptions { get; set; }

        public TaskExceptionList()
        {
            TaskExceptions = new List<Exception>();
        }

        public override string ToString()
        {
            string result = string.Empty;

            foreach (Exception exception in TaskExceptions)
            {
                if (result == string.Empty)
                    result += exception.ToString();
                else
                    result = Environment.NewLine + result + exception.ToString();
            }

            return result;
        }
    }
}
