using StaticMaskingLibrary.MaskingClasses.MaskingAlgoritms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StaticMaskingLibrary.MaskingClasses.MaskingAlgorithms
{
    public static class MaskingAlgorithmsGetter
    {
        private static IEnumerable<IMaskAlgorithm> MaskingAlgorithms { get; set; }
        private static void InitCollection()
        {
            // Создание списка для хранения экземпляров классов, реализующих интерфейс
            List<IMaskAlgorithm> myList = new List<IMaskAlgorithm>();
            // Получение текущей сборки
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            // Получение всех типов из текущей сборки
            Type[] allTypes = currentAssembly.GetTypes();
            // Фильтрация типов по наследованию интерфейса
            Type[] interfaceTypes = allTypes.Where(t => typeof(IMaskAlgorithm).IsAssignableFrom(t) && !t.IsAbstract && t.IsClass).ToArray();
            // Создание экземпляров классов, реализующих интерфейс, и добавление их в список
            foreach (Type type in interfaceTypes)
            {
                IMaskAlgorithm instance = (IMaskAlgorithm)Activator.CreateInstance(type);
                myList.Add(instance);
            }
            MaskingAlgorithms = myList;
        }
        public static IEnumerable<IMaskAlgorithm> GetAlgorithms()
        {
            if (MaskingAlgorithms == null)
                InitCollection();
            return MaskingAlgorithms;
        }
    }
}
