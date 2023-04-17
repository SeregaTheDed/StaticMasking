using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMaskingLibrary.MaskingClasses
{
    public class MaskingColumnModel
    {
        public ForeignKey ForeignKey { get; set; }
        public ColumnTypes ColumnType { get; private set; }
        public Column ColumnReference { get; private set; }
        public MaskingColumnModel(Column column)
        {
            ColumnReference = column;
            if (column.Identity)
                this.ColumnType = ColumnTypes.Identity;
            else if (column.Computed)
                this.ColumnType = ColumnTypes.Computed;
            else
                this.ColumnType = ColumnTypes.Default;
        }
    }
}
