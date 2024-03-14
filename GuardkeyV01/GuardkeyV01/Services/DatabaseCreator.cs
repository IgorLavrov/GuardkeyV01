using GuardkeyV01.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms.Shapes;

namespace GuardkeyV01.Services
{
    public class DatabaseCreator:IDisposable
    {
        readonly SQLiteAsyncConnection  _database;
        public SQLiteAsyncConnection Database => _database;

        public DatabaseCreator(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Note>().Wait();
            _database.CreateTableAsync<Category>().Wait();

        }

        public SQLiteAsyncConnection GetConnection()
        {
            return _database;
        }

        public void Dispose()
        {
            _database.GetConnection().Close();
            _database.GetConnection().Dispose();
        }



    }
}
