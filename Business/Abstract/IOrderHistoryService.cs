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
    public interface IOrderHistoryService
    {
        IDataResult<List<OrderHistory>> GetAll();
        IDataResult<OrderHistory> GetById(int id);
        IDataResult<OrderHistory> GetByFilter<T>(string propertyName, T value);
        IResult Add(OrderHistory orderDto);
        IResult Update(int id, OrderHistory? orderDto);
        IResult Delete(int id);
    }
}
