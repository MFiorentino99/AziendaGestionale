using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models.FileHelpers;

namespace Test.Abstractions
{
    public interface ISelector
    {
        public Type CustomSelector(MultiRecordEngine engine, string recordLine);
    }
}
