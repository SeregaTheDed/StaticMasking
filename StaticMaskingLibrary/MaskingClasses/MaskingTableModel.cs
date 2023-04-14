using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMaskingLibrary.MaskingClasses
{
    public class MaskingTableModel
    {
        public Dictionary<string, MaskingColumnModel> Tables { get; private set; } 
        public MaskingTableModel(Table table)
        {
            Tables = new Dictionary<string, MaskingColumnModel>();
        }
    }
}
