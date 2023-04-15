using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using StaticMaskingLibrary.MaskingClasses;
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
            StaticMasker masker = new StaticMasker("localhost", "exampleMaskingDB");
            foreach (var table in masker.MaskingOptions.Tables)
            {
                Console.WriteLine(table.Key);
                foreach (var column in table.Value.Columns)
                {
                    Console.WriteLine("\t" + column.Key + " - " + column.Value.ColumnType);
                }
            }
            try
            {
                masker.MaskDatabase("exampleMaskingDB_COPY");
            }
            catch (Exception e)
            {
                Console.WriteLine("--------------");
                Console.WriteLine(e.Message);
            }
            
        }

        // Получаем коллекцию скриптов для создания базы данных
        // StringCollection script = db.Script();
    }
}