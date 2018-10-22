using CepExpress.Data.Provider;
using CepExpress.Droid.Providers;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidSQLiteDatabasePathProvider))]

namespace CepExpress.Droid.Providers
{
    sealed class DroidSQLiteDatabasePathProvider : ISQLiteDatabasePathProvider
    {
        public DroidSQLiteDatabasePathProvider()
        {

        }

        public string GetDatabasePath() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CepExpress.db3");
        
    }
}