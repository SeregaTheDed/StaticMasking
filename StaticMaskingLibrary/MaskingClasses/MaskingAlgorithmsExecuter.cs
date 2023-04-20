using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMaskingLibrary.MaskingClasses
{
    internal class MaskingAlgorithmsExecuter
    {
        public MaskingOptions MaskingOptions { get; }
        public MaskingAlgorithmsExecuter(MaskingOptions maskingOptions)
        {
            MaskingOptions = maskingOptions;
        }

        public void Execute() 
        {
            foreach (var tableModel in MaskingOptions.Tables.Values)
            {
                var currentTable = tableModel.TableReference;
                foreach (var columnModel in tableModel.Columns.Values)
                {
                    var currentColumn = columnModel.ColumnReference;
                    if (columnModel.MaskAlgorithm == null)
                        continue;
                    if (columnModel.ColumnType != ColumnTypes.Default)
                        throw new InvalidOperationException($"Колонку {columnModel.ColumnReference.Name} не является изменяемой пользователем колонкой!");
                    if (columnModel.ForeignKey != null)
                        throw new InvalidOperationException($"Для изменения колонки {columnModel.ColumnReference.Name} с внешним ключом {columnModel.ForeignKey.Name} нужно изменять ссылочную колонку!");
                    foreach (var maskedColumnValue in columnModel.MaskAlgorithm.GetMaskedValue())
                    {
                        string query = 
                        $"use [{MaskingOptions.Database.Name}]\n" +
                        $"go\n" +
                        $"update {currentTable.ToString()}\n" +
                        $"set {currentColumn.Name} = {maskedColumnValue}";
                        MaskingOptions.Database.ExecuteNonQuery(query);
                    }
                    
                }
            }
        }
        
    }
}
