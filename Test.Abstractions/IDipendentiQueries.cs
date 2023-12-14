using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Abstractions
{
    public interface IDipendentiQueries
    {
        public Task<IEnumerable<DTODipendente>> GetAll();
        public Task<DTODipendente> GetById(string id);
        public Task<IEnumerable<dynamic>> EmployeeOfOtherSectors(string settore);
        public Task<IEnumerable<DTODipendente>> BestVendor(int min, int max);
        public Task<IEnumerable<dynamic>> ChainedNameSurname();
        public Task<IEnumerable<DTODipendente>> LongestSurname();
        public Task<IEnumerable<dynamic>> CheckEmployeeInGestione();


    }
}
