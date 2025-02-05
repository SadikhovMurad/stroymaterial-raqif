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
    public interface IOrderService
    {
        IDataResult<List<Order>> GetAll();
        IDataResult<List<OrderForListDto>> GetAllOrderWithDetails();
        IDataResult<Order> GetById(Guid id);
        IDataResult<Order> GetByFilter<T>(string propertyName, T value);
        IResult Add(OrderDto orderDto,Guid userId);
        IResult Update(Guid id, Order? order);
        IResult Delete(Guid id);
    }
}
