using Core.Utilities.Results;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEmployeeService
    {
        IDataResult<List<Employee>> GetAll();
        IDataResult<List<OrderHistory>> GetAllOrdersByCoruier(int coruierId);
        IDataResult<Employee> GetById(int id);
        IDataResult<Employee> GetByFilter<T>(string propertyName,T value);
        IResult Add(Employee categoryDto);
        IResult Update(int id, Employee? categoryDto);
        IResult Delete(int id);
    }
}
