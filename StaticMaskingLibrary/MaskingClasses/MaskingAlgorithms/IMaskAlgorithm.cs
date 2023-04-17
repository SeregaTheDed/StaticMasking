using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMaskingLibrary.MaskingClasses.MaskingAlgoritms
{
    public interface IMaskAlgorithm
    {
        public string GetMaskedValue(string value);
    }
}
