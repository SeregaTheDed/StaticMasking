using Microsoft.SqlServer.Management.Smo;
using StaticMaskingLibrary.MaskingClasses.MaskingAlgoritms;
using StaticMaskingLibrary.MaskingClasses.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StaticMaskingLibrary.MaskingClasses
{
    public static class MaskingAlgorithmsFactory
    {
        private static IEnumerable<MaskAlgorithm> MaskingAlgorithmDefinitions { get; set; }
        private static void InitDefinitionsCollection()
        {
            // Создание списка для хранения экземпляров классов, реализующих интерфейс
            List<MaskAlgorithm> myList = new List<MaskAlgorithm>();
            // Получение текущей сборки
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            // Получение всех типов из текущей сборки
            Type[] allTypes = currentAssembly.GetTypes();
            // Фильтрация типов по наследованию интерфейса
            Type[] interfaceTypes = allTypes.Where(t => typeof(MaskAlgorithm).IsAssignableFrom(t) && !t.IsAbstract && t.IsClass).ToArray();
            // Создание экземпляров классов, реализующих интерфейс, и добавление их в список
            foreach (Type type in interfaceTypes)
            {
                var instance = (MaskAlgorithm)Activator.CreateInstance(type, true);
                myList.Add(instance);
            }
            MaskingAlgorithmDefinitions = myList;
        }
        public static MaskAlgorithm GetInstanse(MaskAlgorithmDefinition maskAlgorithmDefinition, Column column)
        {
            Type algorithmType = 
                MaskingAlgorithmDefinitions
                .First(x => x.MaskAlgorithmDefinition.CompareTo(maskAlgorithmDefinition) == 0)
                .GetType();
            return (MaskAlgorithm)Activator.CreateInstance(algorithmType, column);
        }
        public static IEnumerable<MaskAlgorithmDefinition> GetAlgorithmDefinitions()
        {
            if (MaskingAlgorithmDefinitions == null)
                InitDefinitionsCollection();
            return MaskingAlgorithmDefinitions.Select(x => x.MaskAlgorithmDefinition);
        }
    }
}
