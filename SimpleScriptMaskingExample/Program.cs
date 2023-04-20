using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using StaticMaskingLibrary.MaskingClasses;
using StaticMaskingLibrary.MaskingClasses.MaskingAlgorithms;
using System.Collections.Specialized;
using System.Reflection;

namespace SimpleScriptMaskingExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintTablesAndColumns();
        }
        private static void PrintTablesAndColumns()
        {
            StaticMasker masker = new StaticMasker("localhost", "exampleMaskingDB", "exampleMaskingDB_COPY");
            var columnModel = masker.MaskingOptions.Tables["cards"].Columns["number"];
            columnModel.MaskAlgorithm = new LastFourCardNumberMA(columnModel.ColumnReference);
            foreach (var table in masker.MaskingOptions.Tables)
            {
                Console.WriteLine(table.Key);
                foreach (var column in table.Value.Columns)
                {
                    Console.WriteLine($"\t{column.Key} - {column.Value.ColumnType} - {column.Value.ForeignKey}");
                }
            }
            try
            {
                masker.MaskDatabase();
                //masker.MaskingOptions.Database.Drop();
            }
            catch (Exception e)
            {
                //masker.MaskingOptions.Database.Drop();
                Console.WriteLine("--------------");
                Console.WriteLine(e.Message);
            }
            
        }

        // Получаем коллекцию скриптов для создания базы данных
        // StringCollection script = db.Script();
    }
}