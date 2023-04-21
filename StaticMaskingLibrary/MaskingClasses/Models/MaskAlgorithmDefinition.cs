using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMaskingLibrary.MaskingClasses.Models
{
    public class MaskAlgorithmDefinition : IComparable<MaskAlgorithmDefinition>
    {
        public string Description { get; }
        public Guid Guid { get; } = Guid.NewGuid();
        internal MaskAlgorithmDefinition(string Description)
        {
            this.Description = Description;
        }

        public int CompareTo(MaskAlgorithmDefinition? other)
        {
            return this.Guid.CompareTo(other.Guid);
        }
    }
}
