using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal orderDal;
        private readonly IMapper _mapper;

        public OrderManager(IOrderDal orderDal, IMapper mapper)
        {
            this.orderDal = orderDal;
            _mapper = mapper;
        }


        public IResult Add(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            orderDal.Add(order);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Guid id)
        {
            var product = orderDal.Get(x => x.Id == id);
            if (product == null)
            {
                return new ErrorResult("Bele id de sifaris yoxdur");
            }
            orderDal.Delete(product);
            return new SuccessResult(Messages.ProductRemoved);
        }

        public IDataResult<List<Order>> GetAll()
        {
            return new SuccessDataResult<List<Order>>(orderDal.GetAll(), Messages.CategoryListed);
        }

        public IDataResult<Order> GetByFilter<T>(string propertyName, T value)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Order> GetById(Guid id)
        {
            return new SuccessDataResult<Order>(orderDal.Get(o => o.Id == id), Messages.GetProductById);
        }

        public IResult Update(Guid id, Order? order)
        {
            orderDal.Update(order);
            return new SuccessResult(Messages.ProductUpdated);
        }

    }
}
