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
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            orderItem.Price = orderItemDto.Quantity * orderItem.Product.Price;
            _orderItemDal.Add(orderItem);
            return new SuccessResult(Messages.ProductAddedToCart);
        }

        public IResult AddQuantity(int id)
        {
            var orderItem = _orderItemDal.Get(oi => oi.Id == id);
            if(orderItem == null) 
            {
                return new ErrorResult(Messages.ProductNotFound);
            }
            var newOrderItem = new OrderItem()
            {
                Id = id,
                ProductId = orderItem.ProductId,
                Product = orderItem.Product,
                Quantity = orderItem.Quantity + 1,
                Price = orderItem.Price
            };
            _orderItemDal.Update(newOrderItem);
            return new SuccessResult(Messages.ProductQuantityAdded);
        }

        public IResult Delete(int id)
        {
            var orderItem = _orderItemDal.Get(x => x.Id == id);
            if (orderItem == null)
            {
                return new ErrorResult("Bele id de sebet yoxdur");
            }
            _orderItemDal.Delete(orderItem);
            return new SuccessResult(Messages.ProductRemovedToCart);
        }

        public IDataResult<List<OrderItem>> GetAll()
        {
            return new SuccessDataResult<List<OrderItem>>(_orderItemDal.GetAll(), Messages.CategoryListed);
        }

        public IDataResult<OrderItem> GetByFilter<T>(string propertyName, T value)
        {
            throw new NotImplementedException();
        }

        public IDataResult<OrderItem> GetById(int id)
        {
            return new SuccessDataResult<OrderItem>(_orderItemDal.Get(o => o.Id == id), Messages.GetProductById);
        }

        public IResult Update(int id, OrderItem? order)
        {
            _orderItemDal.Update(order);
            return new SuccessResult(Messages.GetOrder);
        }
    }
}
