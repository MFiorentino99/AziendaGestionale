using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Abstractions
{
    public interface IHttpCallMoreThanOneId<T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetOneByIDs(string id,string id2);
        public bool UpdateByIds(string id,string id2, T element);
        public bool CreateElement(T element);
        public bool DeleteOneByIds(string id, string id2);
        public bool ElementExists(string id, DateTime date);
    }
}
