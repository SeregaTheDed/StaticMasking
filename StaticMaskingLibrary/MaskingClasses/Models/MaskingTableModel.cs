using Microsoft.SqlServer.Management.Smo;

namespace StaticMaskingLibrary.MaskingClasses.Models
{
    public class MaskingTableModel
    {
        public Dictionary<string, MaskingColumnModel> Columns { get; private set; }
        public Table TableReference { get; private set; }
        internal MaskingTableModel(Table table)
        {
            TableReference = table;
            Columns = new Dictionary<string, MaskingColumnModel>();
            foreach (Column column in table.Columns)
            {
                Columns[column.Name] = new MaskingColumnModel(column);
            }
            foreach (ForeignKey foreignKey in table.ForeignKeys)
            {
                var columnName = foreignKey.Columns[0].ToString().Trim(new char[] { '[', ']' });
                Columns[columnName].ForeignKey = foreignKey;
            }
        }
        public MaskingColumnModel[] GetEditableColumns()
        {
            return Columns.Values
                .Where(x => x.ColumnType == Enums.ColumnTypes.Default && x.ForeignKey == null)
                .ToArray();
        }
    }
}
