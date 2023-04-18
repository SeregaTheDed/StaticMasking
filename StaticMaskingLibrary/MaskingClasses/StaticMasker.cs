﻿using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Wmi;
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

        ~StaticMasker()
        {
            if (ServerConnection != null)
                ServerConnection.Disconnect();
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








            keyUpdater.ResetForeignKeyActions();
        }
    }
}