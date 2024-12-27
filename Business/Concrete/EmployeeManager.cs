using Business.Abstract;
using Core.Utilities.Results;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        public IResult Add(Employee categoryDto)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Employee>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<OrderHistory>> GetAllOrdersByCoruier(int coruierId)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Employee> GetByFilter<T>(string propertyName, T value)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Employee> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(int id, Employee? categoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
