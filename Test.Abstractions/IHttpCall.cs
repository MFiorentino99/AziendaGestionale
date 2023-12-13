using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Abstractions
{
    public interface IHttpCall<T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetOneByID(string id);
        public bool UpdateOneById(string id,T element);
        public bool CreateElement(T element);
        public bool DeleteOneById(string id);
        public bool ElementExists(string id);


    }
}
