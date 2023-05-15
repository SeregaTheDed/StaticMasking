using Microsoft.SqlServer.Management.Smo;

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
            xfr.Options.SchemaQualifyForeignKeysReferences = true;//?
            xfr.CopyAllColumnEncryptionKey = true;

            xfr.CopyAllObjects = true;//?
            //xfr.Options.DriForeignKeys = true;
            xfr.Options.DriAllKeys = true;

            //Script the transfer. Alternatively perform immediate data transfer   
            // with TransferData method.   
            xfr.ScriptTransfer();
            xfr.TransferData();
        }


    }
}
