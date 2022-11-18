using System;

namespace Sat.Recruitment.Entities.Interface
{
    public interface ILogService
    {
        void Info(string message);
        void Error(Exception e);
    }
}