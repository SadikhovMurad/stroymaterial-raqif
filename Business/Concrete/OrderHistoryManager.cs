using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderHistoryManager : IOrderHistoryService
    {
        private readonly IOrderHistoryDal orderHistoryDal;
        private readonly IMapper _mapper;

        public OrderHistoryManager(IOrderHistoryDal orderHistoryDal, IMapper mapper)
        {
            this.orderHistoryDal = orderHistoryDal;
            _mapper = mapper;
        }

        public IResult Add(OrderHistory orderDto)
        {
            var orderHistory = _mapper.Map<OrderHistory>(orderDto);
            orderHistoryDal.Add(orderHistory);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(int id)
        {
            var orderHistory = orderHistoryDal.Get(x => x.Id == id);
            if (orderHistory == null)
            {
                return new ErrorResult("Bele id de sifaris tarixcesi yoxdur");
            }
            orderHistoryDal.Delete(orderHistory);
            return new SuccessResult(Messages.ProductRemoved);
        }

        public IDataResult<List<OrderHistory>> GetAll()
        {
            return new SuccessDataResult<List<OrderHistory>>(orderHistoryDal.GetAll(), Messages.CategoryListed);
        }

        public IDataResult<OrderHistory> GetByFilter<T>(string propertyName, T value)
        {
            throw new NotImplementedException();
        }

        public IDataResult<OrderHistory> GetById(int id)
        {
            return new SuccessDataResult<OrderHistory>(orderHistoryDal.Get(o => o.Id == id), Messages.GetProductById);
        }

        public IResult Update(int id, OrderHistory? orderDto)
        {
            orderHistoryDal.Update(orderDto);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
