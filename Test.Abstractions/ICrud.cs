using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Abstractions
{
    internal interface ICrud
    {
        public void CreateTable(string tableName);
        public void DeleteTable(string tableName);
        public void AlterTable(string tableName);

    }
}
