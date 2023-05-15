using Microsoft.SqlServer.Management.Smo;
using StaticMaskingLibrary.MaskingClasses.MaskingAlgoritms;
using StaticMaskingLibrary.MaskingClasses.Models;

namespace StaticMaskingLibrary.MaskingClasses.MaskingAlgorithms
{
    public class LastFourCardNumberMA : MaskAlgorithm
    {
        internal LastFourCardNumberMA() : base(null)
        {
            this.maskAlgorithmDefinition = new MaskAlgorithmDefinition
                (
                    "Маскирует номер карты, оставляя последние 4 цифры, а остальные заменяет на символ \"*\""
                );
        }
        public LastFourCardNumberMA(Column column) : base(column) { }

        internal override IEnumerable<MaskedValueModel> GetMaskedValues()
        {
            yield return new MaskedValueModel { MaskedColumn = $"\'************\' + right({this.Column.Name}, 4)" };
        }
    }
}