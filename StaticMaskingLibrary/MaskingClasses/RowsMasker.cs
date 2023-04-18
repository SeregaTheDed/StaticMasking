using Microsoft.SqlServer.Management.Smo;
using StaticMaskingLibrary.MaskingClasses.MaskingAlgoritms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMaskingLibrary.MaskingClasses
{
    internal class RowsMasker
    {
        internal IMaskAlgorithm MaskAlgorithm { get; set; }
        internal RowsMasker(IMaskAlgorithm maskAlgorithm) 
        { 
            this.MaskAlgorithm = maskAlgorithm;
        }
        
        internal void Mask(MaskingColumnModel columnModel)
        {
            if (columnModel.ColumnType != ColumnTypes.Default)
                throw new ArgumentException($"Колонку {columnModel.ColumnReference.Name} не является изменяемой пользователем колонкой!");
            if (columnModel.ForeignKey != null)
                throw new ArgumentException($"Для изменения колонки {columnModel.ColumnReference.Name} с внешним ключом {columnModel.ForeignKey.Name} нужно изменять ссылочную колонку!");
            var column = columnModel.ColumnReference;
        }
        private void MaskColumnWithForeightKey(Column column)
        {
            /*if (column is ForeignKeyColumn)
            // Получаем объект ForeignKey для данной колонки
            ForeignKey fk = column.EnumForeignKeys()[0];

            // Проверяем наличие опции ON UPDATE CASCADE
            if (!fk.Options.HasFlag(ForeignKeyOptions.UpdateCascade))
            {
                // Устанавливаем опцию ON UPDATE CASCADE
                fk.Options |= ForeignKeyOptions.UpdateCascade;
                fk.Alter();
            }*/
        }
    }
}
