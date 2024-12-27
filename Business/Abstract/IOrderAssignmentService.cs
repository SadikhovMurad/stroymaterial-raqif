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
    public interface IOrderAssignmentService
    {
        IDataResult<List<OrderAssignment>> GetAll();
        IDataResult<List<OrderAssignment>> GetAllSuccessOrder();
        IDataResult<List<OrderAssignment>> GetAllNotSuccessOrder();
        IDataResult<OrderAssignment> GetById(int id);
        IDataResult<OrderAssignment> GetByFilter<T>(string propertyName, T value);
        IResult Add(OrderAssignmentDto orderAssignmentDto);
        IResult Update(int id, OrderAssignment? orderAssignment);
        IResult Delete(int id);
    }
}
