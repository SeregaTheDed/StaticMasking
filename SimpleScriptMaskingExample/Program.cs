using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using StaticMaskingLibrary.MaskingClasses;
using StaticMaskingLibrary.MaskingClasses.MaskingAlgorithms;
using StaticMaskingLibrary.MaskingClasses.MaskingAlgoritms;
using StaticMaskingLibrary.MaskingClasses.MaskingResults;
using StaticMaskingLibrary.MaskingClasses.Models;
using System.Collections.Specialized;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace SimpleScriptMaskingExample
{
    public class ConsoleStaticMaskInterface
    {
        private StaticMasker masker;
        private MaskAlgorithmDefinition[] maskAlgorithms = MaskingAlgorithmsFactory.GetAlgorithmDefinitions().ToArray();
        public ConsoleStaticMaskInterface() { }
        public void Start()
        {
            /*
localhost
exampleMaskingDB
exampleMaskingDB_COPY
             */
            Console.WriteLine("localhost");
            Console.WriteLine("exampleMaskingDB");
            Console.WriteLine("exampleMaskingDB_COPY");
            Console.Write("Input DB's server:");
            string serverInstance = Console.ReadLine();//localhost
            Console.Write("Input DB's name:");
            string serverName = Console.ReadLine();//exampleMaskingDB
            Console.Write("Input DB's copy name:");
            string serverNameCopy = Console.ReadLine();//exampleMaskingDB_COPY
            Console.WriteLine("Finding metadata...");
            try
            {
                masker = new StaticMasker(serverInstance, serverName, serverNameCopy);
                PrintSelectTables();
                masker.MaskDatabase();
                PrintMaskingResults(masker.MaskingOptions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            Console.ReadLine();
        }
        private void PrintSelectTables()
        {
            var allTables = masker.MaskingOptions.Tables.Keys.ToArray();
            while (true)
            {
                Console.Clear();
                int i = 0;
                Console.WriteLine("Enter punkt number:");
                foreach (var tableName in allTables)
                {
                    Console.WriteLine($"{i}. {tableName}");
                    i++;
                }
                Console.WriteLine("End. Start masking database.");
                string selectedTableIndex = Console.ReadLine().Trim('.');
                if (selectedTableIndex.ToLower() == "end")
                    break;
                if (int.TryParse(selectedTableIndex, out int index) && !(index < 0 || index >= allTables.Length))
                {
                    var selectedTableName = allTables[index];
                    var selectedTableModel = masker.MaskingOptions.Tables[selectedTableName];
                    PrintSelectColumn(selectedTableModel);
                }
                else
                {
                    Console.WriteLine("Input error! Enter punkt number. Press any key...");
                    Console.ReadKey();
                }
            }
        }
        private void PrintSelectColumn(MaskingTableModel maskingTableModel)
        {
            var columnsToEdit = maskingTableModel.GetEditableColumns();
            while (true)
            {
                Console.Clear();
                int i = 0;
                Console.WriteLine("Enter punkt number:");
                foreach (var columnModel in columnsToEdit)
                {
                    Console.WriteLine($"{i}. {columnModel.ColumnReference.Name} - " +
                        (columnModel.MaskAlgorithm != null 
                        ?
                        "Algorithm selected"
                        :
                        "Algorithm not selected"));
                    i++;
                }
                Console.WriteLine("End. Return to select table.");
                string selectedTableIndex = Console.ReadLine().Trim('.');
                if (selectedTableIndex.ToLower() == "end")
                    break;
                if (int.TryParse(selectedTableIndex, out int index) && !(index < 0 || index >= columnsToEdit.Length))
                {
                    var selectedColumnModel = columnsToEdit[index];
                    PrintSelectMaskAlgorithmToColumn(selectedColumnModel);
                }
                else
                {
                    Console.WriteLine("Input error! Enter punkt number. Press any key...");
                    Console.ReadKey();
                }
            }
        }
        private void PrintSelectMaskAlgorithmToColumn(MaskingColumnModel maskingColumnModel)
        {
            while (true)
            {
                Console.Clear();
                int i = 0;
                Console.WriteLine("Enter punkt number:");
                foreach (var maskAlgorithm in maskAlgorithms)
                {
                    Console.WriteLine($"{i}. {maskAlgorithm.Description}");
                    i++;
                }
                Console.WriteLine("End. Return to select colunmns.");
                string selectedTableIndex = Console.ReadLine().Trim('.');
                if (selectedTableIndex.ToLower() == "end")
                    break;
                if (int.TryParse(selectedTableIndex, out int index) && !(index < 0 || index >= maskAlgorithms.Length))
                {
                    var selectedMaskAlgorithm = maskAlgorithms[index];
                    SetMaskAlgorithmToColumnModel(selectedMaskAlgorithm, maskingColumnModel);
                    Console.WriteLine("Algorithm successful selected! Press any key...");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("Input error! Enter punkt number. Press any key...");
                    Console.ReadKey();
                }
            }
        }
        private void SetMaskAlgorithmToColumnModel(MaskAlgorithmDefinition maskAlgorithmDefinition, MaskingColumnModel maskingColumnModel)
        {
            var maskingAlgorithmInstanse = MaskingAlgorithmsFactory.GetInstanse(maskAlgorithmDefinition, maskingColumnModel.ColumnReference);
            maskingColumnModel.MaskAlgorithm = maskingAlgorithmInstanse;
        }
        private void PrintMaskingResults(MaskingOptions maskingOptions)
        {
            foreach (var table in maskingOptions.Tables)
            {
                Console.WriteLine(table.Key);
                foreach (var column in table.Value.Columns)
                {
                    string maskingResult;
                    if (column.Value.MaskingResult is MaskingResultNotMasked)
                    {
                        maskingResult = "Not masked";
                    }
                    else if (column.Value.MaskingResult is SuccessfulMaskingResult)
                    {
                        maskingResult = "Successful masked";
                    }
                    else if (column.Value.MaskingResult is FailedMaskingResultWithException failed)
                    {
                        maskingResult = "Masked with exception: " + failed.ResultException.Message;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                    Console.WriteLine($"\t{column.Key} - {maskingResult}");
                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleStaticMaskInterface consoleStaticMaskInterface = new ConsoleStaticMaskInterface();
            consoleStaticMaskInterface.Start();
            //PrintTablesAndColumns();
        }
        private static void PrintTablesAndColumns()
        {
            /*var algorithms = MaskingAlgorithmsFactory.GetAlgorithmDefinitions();
            foreach (var algorithm in algorithms)
            {
                Console.WriteLine(algorithm.Description);
            }
            return;*/
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
                //masker.MaskDatabase();
                //masker.MaskingOptions.Database.Drop();
            }
            catch (Exception e)
            {
                //masker.MaskingOptions.Database.Drop();
                Console.WriteLine("--------------");
                Console.WriteLine(e.Message);
            }
            
        }

    }
}