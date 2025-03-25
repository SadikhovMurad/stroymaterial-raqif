using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderItemManager : IOrderItemService
    {
        private readonly IOrderItemDal _orderItemDal;
        private readonly IMapper _mapper;

        public OrderItemManager(IOrderItemDal orderItemDal, IMapper mapper)
        {
            _orderItemDal = orderItemDal;
            _mapper = mapper;
        }

        public IResult Add(OrderItemDto orderItemDto)
        {
            throw new NotImplementedException();
        }

        public IResult AddQuantity(int id)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<OrderItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<OrderItem> GetByFilter<T>(string propertyName, T value)
        {
            throw new NotImplementedException();
        }

        public IDataResult<OrderItem> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(int id, OrderItem? orderItem)
        {
            throw new NotImplementedException();
        }
    }
}
