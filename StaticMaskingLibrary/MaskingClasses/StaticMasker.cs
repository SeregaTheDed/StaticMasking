using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Wmi;
using StaticMaskingLibrary.Utilities;

namespace StaticMaskingLibrary.MaskingClasses
{
    public class StaticMasker
    {
        public MaskingOptions MaskingOptions { get; private set; }
        internal Server Server { get; }
        internal Database SelectedDatabase { get; }
        private ServerConnection ServerConnection { get; }
        public StaticMasker(string serverInstance, string databaseName)
        {
            ServerConnection = new ServerConnection(serverInstance);
            Server = new Server(ServerConnection);
            SelectedDatabase = Server.Databases[databaseName];
            if (SelectedDatabase == null)
                throw new ArgumentException($"Database \"{databaseName}\" not exists!");
            MaskingOptions = new MaskingOptions(SelectedDatabase);
        }
        ~StaticMasker()
        {
            ServerConnection.Disconnect();
        }
        public void MaskDatabase(string newDatabaseName)
        {
            if (Server.Databases.Contains(newDatabaseName))
                throw new ArgumentException($"Database \"{newDatabaseName}\" already exists!");

            DatabaseCopier databaseCopier = new DatabaseCopier(Server, SelectedDatabase);
            databaseCopier.Copy(newDatabaseName);
        }
        public string MaskDatabaseAndGetScript()
        {
            throw new NotImplementedException();
        }
    }
}