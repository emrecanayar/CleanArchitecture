﻿namespace Core.CrossCuttingConcerns.Logging.DbLog
{
    public class LogDatabaseSettings : ILogDatabaseSettings
    {
        public string LogCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
