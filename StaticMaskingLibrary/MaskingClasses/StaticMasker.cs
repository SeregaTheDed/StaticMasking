using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Wmi;

namespace StaticMaskingLibrary.MaskingClasses
{
    public class StaticMasker
    {
        public MaskingOptions MaskingOptions { get; private set; }
        public string ServerInstance { get; }
        public string DatabaseName { get; }
        public StaticMasker(string serverInstance = "localhost", string databaseName = "exampleMaskingDB")
        {
            ServerInstance = serverInstance;
            DatabaseName = databaseName;
            InitMaskingOptions();
        }
        private void InitMaskingOptions()
        {
            ServerConnection conn = new ServerConnection(ServerInstance);
            Server srv = new Server(conn);
            Database database = srv.Databases[DatabaseName];
            if (database == null)
                throw new ArgumentException($"Database \"{DatabaseName}\" not exists!");
            MaskingOptions = new MaskingOptions(database);
        }
        public string GetScript()
        {
            throw new NotImplementedException();
        }
    }
}