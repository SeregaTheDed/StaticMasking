using Microsoft.SqlServer.Management.Smo;
using StaticMaskingLibrary.MaskingClasses.Models;

namespace StaticMaskingLibrary.MaskingClasses
{
    public class MaskingOptions
    {
        public Dictionary<string, MaskingTableModel> Tables { get; set; }
        public Database Database { get; }

        public MaskingOptions(Database database)
        {
            Database = database;
            InitTables();
        }

        private void InitTables()
        {
            Tables = new Dictionary<string, MaskingTableModel>();
            foreach (Table table in this.Database.Tables)
            {
                if (table.IsSystemObject)
                    continue;
                Tables[table.Name] = new MaskingTableModel(table);
            }
        }


    }
}
