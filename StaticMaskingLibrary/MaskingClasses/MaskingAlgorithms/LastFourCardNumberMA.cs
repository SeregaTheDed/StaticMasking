using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using StaticMaskingLibrary.MaskingClasses.MaskingAlgoritms;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticMaskingLibrary.MaskingClasses.MaskingAlgorithms
{
    public class LastFourCardNumberMA : MaskAlgorithm
    {

        public LastFourCardNumberMA(Column column) : base(column) { }

        public override IEnumerable<string> GetMaskedValue()
        {
            yield return $"\'************\' + right({this.Column.Name}, 4)";
        }
    }
}
/*
update dbo.users
set FIO = replace(FIO, N'ч', N'чч')

update users
set FIO = replace(FIO, N'чч', N'ч')

select*
from users

select replace(replace(FIO, N'ч', N'чч'), N'чч', N'ч')
from users

select '************' + right('1234567812345678', 4)
*/