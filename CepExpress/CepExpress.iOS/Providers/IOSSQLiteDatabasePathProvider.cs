using CepExpress.Data.Provider;
using CepExpress.IOS.Providers;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSSQLiteDatabasePathProvider))]

namespace CepExpress.IOS.Providers
{
    sealed class IOSSQLiteDatabasePathProvider : ISQLiteDatabasePathProvider
    {
        public IOSSQLiteDatabasePathProvider()
        {

        }

        public string GetDatabasePath()
        {
            var databaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library", "Databases");

            if (!Directory.Exists(databaseFolder))
            {
                Directory.CreateDirectory(databaseFolder);
            }

            return Path.Combine(databaseFolder, "CepExpress.db3");
        }

    }
}