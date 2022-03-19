using System;
using System.IO;
using SensoStat.Mobile.Repositories.Interfaces;
using SQLite;

namespace SensoStat.Mobile.Repositories
{
    public class SqliteConnectionService : IDatabase
    {
        public SQLiteConnection GetConnection()
        {
            var filename = "DemoDiiage.db";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, filename);
            var connection = new SQLite.SQLiteConnection(path);
            return connection;
        }
    }
}

