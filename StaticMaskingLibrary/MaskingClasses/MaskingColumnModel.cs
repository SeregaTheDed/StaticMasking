using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMaskingLibrary.MaskingClasses
{
    public class MaskingColumnModel
    {
        public ColumnType ColumnType { get; private set; }



        public MaskingColumnModel(Column column)
        {
            if (column.Identity)
                this.ColumnType = ColumnType.Identity;
            else if (column.Computed)
                this.ColumnType = ColumnType.Computed;
            else
                this.ColumnType = ColumnType.Default;
        }
    }
}
