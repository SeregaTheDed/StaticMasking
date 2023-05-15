using Microsoft.SqlServer.Management.Smo;
using StaticMaskingLibrary.MaskingClasses.Enums;
using StaticMaskingLibrary.MaskingClasses.MaskingAlgoritms;
using StaticMaskingLibrary.MaskingClasses.MaskingResults;

namespace StaticMaskingLibrary.MaskingClasses.Models
{
    public class MaskingColumnModel
    {
        public ForeignKey ForeignKey { get; set; }
        public ColumnTypes ColumnType { get; private set; }
        public Column ColumnReference { get; private set; }
        public MaskAlgorithm MaskAlgorithm { get; set; }
        public MaskingResult MaskingResult { get; internal set; } = new MaskingResultNotMasked();
        internal MaskingColumnModel(Column column)
        {
            ColumnReference = column;
            if (column.Identity)
                ColumnType = ColumnTypes.Identity;
            else if (column.Computed)
                ColumnType = ColumnTypes.Computed;
            else
                ColumnType = ColumnTypes.Default;
        }
    }
}
