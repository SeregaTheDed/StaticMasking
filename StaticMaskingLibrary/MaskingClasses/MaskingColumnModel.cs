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
        public ColumnType columnType { get; private set; }

        public MaskingColumnModel(Column column)
        {
            
        }
    }
}
