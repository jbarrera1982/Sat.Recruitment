using Sat.Recruitment.Entities.Interface;
using System;
using System.Diagnostics;

namespace Sat.Recruitment.Entities.Class
{
    public class LogService : ILogService
    {
        public void Info(string message)
        {
            Debug.WriteLine(message, "Info");            
        }
        public void Error(Exception e)
        {
            Debug.WriteLine(e.Message, "Error");

        }
    }
}
