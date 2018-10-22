
using CepExpress.Data.Provider;
using ConsultarCep.Service.Model;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CepExpress.Data
{
    class DatabaseService
    {
        private static Lazy<DatabaseService> _Lazy = new Lazy<DatabaseService>(() => new DatabaseService());

        public static DatabaseService Current { get => _Lazy.Value; }

        private readonly SQLiteConnection _SQLiteConnection;

        public DatabaseService()
        {
            var dbPath = DependencyService.Get<ISQLiteDatabasePathProvider>().GetDatabasePath();

            _SQLiteConnection = new SQLiteConnection(dbPath);
            _SQLiteConnection.CreateTable<Endereco>();
        }

        public bool Save(Endereco endereco) => _SQLiteConnection.InsertOrReplace(endereco) > 0;

        public List<Endereco> GetAll() => _SQLiteConnection.Table<Endereco>().ToList();

        public Endereco GetById(Guid id) => _SQLiteConnection.Find<Endereco>(id);
    }
}
