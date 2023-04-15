using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMaskingLibrary.Utilities
{
    internal class DatabaseCopier
    {
        public Server Server { get; }
        public Database DatabaseToCopy { get; }
        public DatabaseCopier(Server server, Database databaseToCopy) 
        {
            Server = server;
            DatabaseToCopy = databaseToCopy;
        }

        public void Copy(string copiedDatabaseName)
        {
            //Create a new database that is to be destination database.   
            Database dbCopy;
            dbCopy = new Database(Server, copiedDatabaseName);
            dbCopy.Create();
            //Define a Transfer object and set the required options and properties.   
            Transfer xfr;
            xfr = new Transfer(DatabaseToCopy);
            xfr.CopyAllTables = true;
            xfr.Options.WithDependencies = true;
            xfr.Options.ContinueScriptingOnError = true;
            xfr.DestinationDatabase = copiedDatabaseName;
            xfr.DestinationServer = Server.Name;
            xfr.DestinationLoginSecure = true;
            xfr.CopySchema = true;
            //Script the transfer. Alternatively perform immediate data transfer   
            // with TransferData method.   
            xfr.ScriptTransfer();
            xfr.TransferData();
        }

        
    }
}
