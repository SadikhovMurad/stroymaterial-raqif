using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
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
        private readonly IEmployeeDal employeeDal;
        private readonly IOrderHistoryDal orderHistoryDal;
        private readonly IMapper _mapper;

        public EmployeeManager(IEmployeeDal employeeDal, IMapper mapper, IOrderHistoryDal orderHistoryDal)
        {
            this.employeeDal = employeeDal;
            _mapper = mapper;
            this.orderHistoryDal = orderHistoryDal;
        }

        public IResult Add(Employee employee)
        {
            employeeDal.Add(employee);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(int id)
        {
            var employee = employeeDal.Get(x => x.Id == id);
            if (employee == null)
            {
                return new ErrorResult("Hal hazirda bele bir kuryer yoxdur");
            }
            employeeDal.Delete(employee);
            return new SuccessResult(Messages.ProductRemoved);
        }

        public IDataResult<List<Employee>> GetAll()
        {
            return new SuccessDataResult<List<Employee>>(employeeDal.GetAll(), Messages.CategoryListed);
        }

        public IDataResult<List<OrderHistory>> GetAllOrdersByCoruier(int coruierId)
        {
            var orderHistory = orderHistoryDal.GetAll(oh=>oh.CoruierId == coruierId);
            return new SuccessDataResult<List<OrderHistory>>(orderHistory, Messages.ProductListed);
        }

        public IDataResult<Employee> GetByFilter<T>(string propertyName, T value)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Employee> GetById(int id)
        {
            return new SuccessDataResult<Employee>(employeeDal.Get(e => e.Id == id), Messages.GetProductById);
        }

        public IResult Update(int id, Employee? employee)
        {
            employeeDal.Update(employee);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
