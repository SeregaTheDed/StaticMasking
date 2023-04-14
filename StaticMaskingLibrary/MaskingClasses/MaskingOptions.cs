using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace StaticMaskingLibrary.MaskingClasses
{
    public class MaskingOptions
    {
        public string ServerInstance { get; }
        public string DatabaseName { get; }
        public Dictionary<string, Table> Tables { get; set; } = new Dictionary<string, Table>();
        public MaskingOptions(string serverInstance = "localhost", string databaseName = "exampleDB")
        {
            ServerInstance = serverInstance;
            DatabaseName = databaseName;
        }
        
        
    }
}
