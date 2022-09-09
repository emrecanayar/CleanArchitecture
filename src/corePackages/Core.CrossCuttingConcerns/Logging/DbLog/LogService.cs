using Core.CrossCuttingConcerns.Logging.DbLog.Dto;
using Core.CrossCuttingConcerns.Logging.DbLog.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Core.CrossCuttingConcerns.Logging.DbLog
{
    public class LogService : ILogService
    {
        private readonly ILogger _logger;
        private readonly IMongoCollection<Log> _logCollection;
        private const string STR_DATE_FORMAT = "yyyyMMdd";

        public LogService(ILogger _logger, IMongoCollection<Log> logCollection, ILogDatabaseSettings logDatabaseSettings)
        {
            var client = new MongoClient(logDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(logDatabaseSettings.DatabaseName);
            _logCollection = database.GetCollection<Log>(logDatabaseSettings.LogCollectionName);
        }
        public async Task CreateLog(LogDto logDto)
        {

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
