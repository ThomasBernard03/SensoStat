using System;
using System.Collections.Generic;
using SensoStat.Mobile.Repositories.Interfaces;
using SQLite;

namespace SensoStat.Mobile.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly IDatabase _sqLite;
        private readonly SQLiteConnection _connection;
        public Repository(IDatabase sQLite)
        {
            _sqLite = sQLite;
            _connection = _sqLite.GetConnection();
            _connection.CreateTable<T>();
        }

        public void Clear()
        {
            Type type = typeof(T);
            string table = type.Name;
            _connection.Execute($"DELETE FROM {table}");
        }

        public void Delete(T value)
        {
            _connection.Delete(value);
        }

        public T GetById(int id)
        {
            return _connection.Find<T>(id);
        }

        public List<T> Get()
        {
            return _connection.Table<T>().ToList();
        }

        public T Insert(T value)
        {
            _connection.Insert(value);
            return value;
        }

        public T Update(T value)
        {
            _connection.Update(value);
            return value;
        }
    }
}

