using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using StaticMaskingLibrary.MaskingClasses;
using System.Collections.Specialized;

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
            StaticMasker masker = new StaticMasker();

            foreach (var table in masker.MaskingOptions.Tables)
            {
                Console.WriteLine(table.Key);
                foreach (var column in table.Value.Columns)
                {
                    Console.WriteLine("\t" + column.Key + " - " + column.Value.ColumnType);
                }
            }
        }
        void test()
        {
            // Создаем новый объект ServerConnection
            ServerConnection conn = new ServerConnection("localhost");

            // Создаем новый объект Server
            Server srv = new Server(conn);

            // Устанавливаем свойства базы данных
            Database db = new Database(srv, "TestDB");
            if (srv.Databases.Contains("TestDB") == false)
            {
                db.Create();
            }

            // Получаем коллекцию скриптов для создания базы данных
            StringCollection script = db.Script();

            // Выводим скрипт на экран
            foreach (string line in script)
            {
                Console.WriteLine(line);
            }
        }
    }
}