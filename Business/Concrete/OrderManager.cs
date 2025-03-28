﻿using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Entity.DtoS;
using Microsoft.AspNetCore.Mvc.Formatters;
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
        private readonly ICartDal cartDal;
        private readonly IMapper _mapper;

        public OrderManager(IOrderDal orderDal, IMapper mapper, ICartDal cartDal)
        {
            this.orderDal = orderDal;
            _mapper = mapper;
            this.cartDal = cartDal;
        }


        public IResult Add(OrderDto orderDto, Guid userId)
        {
            orderDal.CreateOrder(orderDto,userId);
            return new SuccessResult("Sifaris ugurla verildi");
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

        public IDataResult<List<OrderForListDto>> GetAllOrderWithDetails()
        {
            return new SuccessDataResult<List<OrderForListDto>>(orderDal.GetAllOrderWithDetails(), "Butun sifarisler getirildi");
        }

        public IDataResult<Order> GetByFilter<T>(string propertyName, T value)
        {
            throw new NotImplementedException();
        }

        public IDataResult<OrderForListDto> GetById(Guid id)
        {
            return new SuccessDataResult<OrderForListDto>(orderDal.GetOrderById(id), Messages.GetProductById);
        }

        public IResult SuccessOrder(Guid id)
        {
            orderDal.ChangeStatus(id);
            return new SuccessResult("Okay");
        }

        public IResult Update(Guid id, Order? order)
        {
            orderDal.Update(order);
            return new SuccessResult(Messages.ProductUpdated);
        }

    }
}
