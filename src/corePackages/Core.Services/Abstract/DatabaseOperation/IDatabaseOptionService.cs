namespace Core.Services.Abstract.DatabaseOperation
{
    public interface IDatabaseOptionService
    {
        Task DeleteOutdatedLogData();
    }
}