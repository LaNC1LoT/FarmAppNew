using FarmApp.Domain.Core.Entity;
using FarmApp.Domain.Core.EnumHelpers;
using FarmApp.Infrastructure.Data.Contexts;
using System;

namespace FarmAppServer.Services
{
    public interface ILoggerDb
    {
        void WriteRequest(Log log);
        void WriteResponse(Log log);
        void WriteException(Log log);
    }

    public class LoggerDb : ILoggerDb
    {
        private Guid _groupLogId = Guid.NewGuid();
        private readonly FarmAppContext _farmAppContext;
        public LoggerDb(FarmAppContext farmAppContext)
        {
            _farmAppContext = farmAppContext;
        }

        public void WriteRequest(Log log)
        {
            log.LogType = LogType.Request.ToString();
            SaveLog(log);
        }

        public void WriteResponse(Log log)
        {
            log.LogType = LogType.Response.ToString();
            SaveLog(log);
        }

        public void WriteException(Log log)
        {
            log.LogType = LogType.Exception.ToString();
            SaveLog(log);
        }

        private void SaveLog(Log log)
        {
            log.CreateDate = DateTime.Now;
            log.GroupLogId = _groupLogId;
            _farmAppContext.Add(log);
            _farmAppContext.SaveChanges();
        }
    }
}
