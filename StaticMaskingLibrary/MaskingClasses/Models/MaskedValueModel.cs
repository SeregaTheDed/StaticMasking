using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMaskingLibrary.MaskingClasses.Models
{
    public class MaskedValueModel
    {
        public string MaskedColumn { get; set; }
        public string? Where { get; set; }
    }
}
