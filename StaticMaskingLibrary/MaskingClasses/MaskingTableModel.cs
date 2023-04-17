﻿using Microsoft.SqlServer.Management.Smo;

namespace StaticMaskingLibrary.MaskingClasses
{
    public class MaskingTableModel
    {
        public Dictionary<string, MaskingColumnModel> Columns { get; private set; } 
        public Table TableReference { get; private set; }
        public MaskingTableModel(Table table)
        {
            TableReference = table;
            Columns = new Dictionary<string, MaskingColumnModel>();
            foreach (Column column in table.Columns)
            {
                Columns[column.Name] = new MaskingColumnModel(column);
            }
            foreach (ForeignKey foreignKey in table.ForeignKeys)
            {
                var columnName = foreignKey.Columns[0].ToString().Trim(new char[] {'[', ']'});
                Columns[columnName].ForeignKey = foreignKey;
            }
        }
    }
}
