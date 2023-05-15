using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using StaticMaskingLibrary.Utilities;

namespace StaticMaskingLibrary.MaskingClasses
{
    public class StaticMasker
    {
        public MaskingOptions MaskingOptions { get; private set; }
        internal Server Server { get; }
        internal Database SelectedDatabase { get; private set; }
        private ServerConnection ServerConnection { get; }

        private ForeignKeyUpdater keyUpdater;
        public StaticMasker(string serverInstance, string databaseName, string newDatabaseName)
        {
            ServerConnection = new ServerConnection(serverInstance);
            Server = new Server(ServerConnection);
            SelectedDatabase = Server.Databases[databaseName];
            if (SelectedDatabase == null)
                throw new ArgumentException($"Database \"{databaseName}\" not exists!");
            InitNewDatabase(newDatabaseName);
        }

        private void InitNewDatabase(string newDatabaseName)
        {
            if (Server.Databases.Contains(newDatabaseName))
                throw new ArgumentException($"Database \"{newDatabaseName}\" already exists!");
            DatabaseCopier databaseCopier = new DatabaseCopier(Server, SelectedDatabase);
            databaseCopier.Copy(newDatabaseName);
            SelectedDatabase = Server.Databases[newDatabaseName];
            MaskingOptions = new MaskingOptions(SelectedDatabase);
            keyUpdater = new ForeignKeyUpdater(MaskingOptions);

        }

        public void MaskDatabase()
        {
            keyUpdater.ChangeForeignKeyActions();

            MaskingAlgorithmsExecuter maskingAlgorithmsExecuter = new MaskingAlgorithmsExecuter(MaskingOptions);
            maskingAlgorithmsExecuter.Execute();

            keyUpdater.ResetForeignKeyActions();

            ServerConnection.Disconnect();
        }
    }
}