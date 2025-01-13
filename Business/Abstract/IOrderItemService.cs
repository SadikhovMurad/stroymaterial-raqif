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
    public interface IOrderItemService
    {
        IDataResult<List<OrderItem>> GetAll();
        IDataResult<OrderItem> GetById(int id);
        IDataResult<OrderItem> GetByFilter<T>(string propertyName, T value);
        IResult Add(OrderItemDto orderItemDto);
        IResult Update(int id, OrderItem? orderItem);
        IResult Delete(int id);
    }
}
