using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Abstractions
{
    public interface IFattureQueries
    {
        public Task<IEnumerable<DTOFattura>> GetAll();
        public Task<DTOFattura> GetById(string id,string data);
        public Task<IEnumerable<dynamic>> AnnualIncome();
        public Task<IEnumerable<dynamic>> AnnualIncomePerClient();
        public Task<IEnumerable<dynamic>> AnnualIncomePerClientWithDetails();
        public DateTime DateConverter(string data);

    }
}
