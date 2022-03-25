using SQLite;

namespace SensoStat.Mobile.Repositories.Interfaces
{
    public interface IDatabase
    {
        SQLiteConnection GetConnection();
    }
}

