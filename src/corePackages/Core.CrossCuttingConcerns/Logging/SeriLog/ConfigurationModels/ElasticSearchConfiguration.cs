namespace Core.CrossCuttingConcerns.Logging.SeriLog.ConfigurationModels
{
    public class ElasticSearchConfiguration
    {
        public string ConnectionString { get; set; }

        public ElasticSearchConfiguration()
        {
            this.ConnectionString = String.Empty;
        }
    }
}