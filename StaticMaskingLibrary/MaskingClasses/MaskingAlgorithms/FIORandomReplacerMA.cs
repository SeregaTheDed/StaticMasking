﻿using Microsoft.SqlServer.Management.Smo;
using StaticMaskingLibrary.MaskingClasses.MaskingAlgoritms;
using StaticMaskingLibrary.MaskingClasses.Models;
using StaticMaskingLibrary.utils;
using System.Text.Json;

namespace StaticMaskingLibrary.MaskingClasses.MaskingAlgorithms
{
    public class FIORandomReplacerMA : MaskAlgorithm
    {
        private string[] Lastnames;
        private string[] Names;
        private string[] Patronymic;
        private Random rnd = new Random();
        internal FIORandomReplacerMA() : base(null)
        {
            this.maskAlgorithmDefinition = new MaskAlgorithmDefinition
                (
                    "Заменяет фамилию, имя и отчество в строчках на случайные(используется словарь)"
                );
        }
        public FIORandomReplacerMA(Column column) : base(column)
        {
            this.Names = JsonSerializer.Deserialize<string[]>(File.ReadAllText("Data/Names.json"));
            this.Patronymic = JsonSerializer.Deserialize<string[]>(File.ReadAllText("Data/Patronymic.json"));
            this.Lastnames = JsonSerializer.Deserialize<string[]>(File.ReadAllText("Data/Lastnames.json"));
        }
        private IEnumerable<MaskedValueModel> Generate(string[] array)
        {
            string[] randomedArray = (string[])array.Clone();
            rnd.Shuffle(randomedArray);
            for (int i = 0; i < array.Length; i++)
            {
                yield return new MaskedValueModel
                {
                    MaskedColumn = $"replace({this.Column.Name}, N'{array[i]}', N'{randomedArray[i]}')"
                };

            }
        }

        internal override IEnumerable<MaskedValueModel> GetMaskedValues()
        {
            var collections = new string[][]
            {
                Lastnames,
                Names,
                Patronymic,
            };
            foreach (var collection in collections)
            {
                foreach (var item in Generate(collection))
                {
                    yield return item;
                }
            }

        }
    }
}