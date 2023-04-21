using Microsoft.SqlServer.Management.Smo;
using StaticMaskingLibrary.MaskingClasses.Models;
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
        protected MaskAlgorithmDefinition maskAlgorithmDefinition = new MaskAlgorithmDefinition("Описание алгоритма маскирования");
        public virtual MaskAlgorithmDefinition MaskAlgorithmDefinition { get => maskAlgorithmDefinition; }
        //Более применим фабричный метод, но решил попробовать так
        public MaskAlgorithm(Column column)
        {
            Column = column;
        }
        internal abstract IEnumerable<MaskedValueModel> GetMaskedValues();
    }
}
