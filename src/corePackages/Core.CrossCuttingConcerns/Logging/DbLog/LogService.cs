using Core.CrossCuttingConcerns.Logging.DbLog.Dto;
using Core.CrossCuttingConcerns.Logging.DbLog.Models;
using Core.Helpers.Helpers;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Core.CrossCuttingConcerns.Logging.DbLog
{
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;
        private readonly IMongoCollection<Log> _logCollection;


        public LogService(ILogger<LogService> logger, ILogDatabaseSettings logDatabaseSettings)
        {
            var client = new MongoClient(logDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(logDatabaseSettings.DatabaseName);
            _logCollection = database.GetCollection<Log>(logDatabaseSettings.LogCollectionName);
            _logger = logger;
        }
        public async Task CreateLog(LogDto logDto)
        {
            Log log = logDto.ToMap<Log>();
            await LogToDb(log);
        }

        private async Task LogToDb(Log log)
        {
            try
            {
                await _logCollection.InsertOneAsync(log);
            }
            catch (Exception exception)
            {
                _logger.LogError($"DB Logging Exception: {exception.Message} {Environment.NewLine} " +
                    $"Source: {exception.Source} {Environment.NewLine}" +
                    $"Stack Tree: {exception.StackTrace}");
            }

        }
    }
}
