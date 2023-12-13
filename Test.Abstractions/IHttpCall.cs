using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test.Abstractions
{
    public interface IHttpCall<T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetOneByID(string id);
        public Task<bool> UpdateOneById(string id,T element);
        public Task<bool> CreateElement(T element);
        public Task<bool> DeleteOneById(string id);
        public bool ElementExists(string id);


    }
}
