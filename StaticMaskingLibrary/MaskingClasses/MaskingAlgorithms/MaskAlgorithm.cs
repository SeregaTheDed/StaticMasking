using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMaskingLibrary.MaskingClasses.MaskingAlgoritms
{
    public abstract class MaskAlgorithm
    {
        protected virtual Column Column { get; }
        public MaskAlgorithm(Column column)
        {
            Column = column;
        }
        public abstract IEnumerable<string> GetMaskedValue();
    }
}
