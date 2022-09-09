using Core.CrossCuttingConcerns.Logging.DbLog.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.DbLog
{
    public interface ILogService
    {
        Task CreateLog(LogDto logDto);
    }
}
