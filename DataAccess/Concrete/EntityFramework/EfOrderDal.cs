using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DtoS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfRepositoryBase<Order, ModelDbContext>, IOrderDal
    {
        private readonly ICartDal _cartDal;

        public EfOrderDal(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public List<OrderForListDto> GetAllOrderWithDetails()
        {
            using var context = new ModelDbContext();

            var orders = context.Orders.Include(o => o.Cart).Include(o => o.User).Include(o=>o.Cart.CartItems);
            List<OrderForListDto> orderForListDtos =new List<OrderForListDto>();
            
            foreach (var order in orders)
            {
                OrderForListDto dto = new OrderForListDto()
                {
                    OrderId = order.Id,
                    CartId = order.CartId,
                    CartItemDtos = _cartDal.GetAllCartItemsByUserId(order.UserId),
                    OrderDate = order.OrderDate,
                    Location = order.Location,
                    UserEmail = order.User.Email,
                    UserName = order.User.Firstname,
                    UserPhoneNumber = order.PhoneNumber,
                    Notification = order.Notification,
                    TotalAmount = order.TotalAmount,
                    Success = order.IsSuccess
                };
                orderForListDtos.Add(dto);
            }
            return orderForListDtos;
        }
    }
}
