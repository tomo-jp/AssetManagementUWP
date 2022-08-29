using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementUWP.Models
{
    internal class ResultModel
    {
        public int Year { get; }
        public int TotalAsset { get; }
        public int Dividend { get; }

        public ResultModel(int year, int totalAsset, int dividend)
        {
            Year = year;
            TotalAsset = totalAsset;
            Dividend = dividend;
        }
    }
}
