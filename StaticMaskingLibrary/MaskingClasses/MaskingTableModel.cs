using Microsoft.SqlServer.Management.Smo;

namespace StaticMaskingLibrary.MaskingClasses
{
    public class MaskingTableModel
    {
        public Dictionary<string, MaskingColumnModel> Columns { get; private set; } 
        public MaskingTableModel(Table table)
        {
            Columns = new Dictionary<string, MaskingColumnModel>();
            foreach (Column column in table.Columns)
            {
                Columns[column.Name] = new MaskingColumnModel(column);
            }
        }
    }
}
