using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DtoS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
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

        public void CreateOrder(OrderDto order, Guid userId)
        {
            using var context = new ModelDbContext();

            var cart = context.Carts.FirstOrDefault(c => c.UserId == userId);
            var user = context.Users.FirstOrDefault(u => u.Id == userId);
            var newOrder = new Order()
            {
                Cart = cart,
                CartId = cart.Id,
                UserId = userId,
                User = user,
                Location = order.Location,
                Notification = order.Notification,
                OrderDate = DateTime.Now.Date,
                IsSuccess = true,
                PhoneNumber = user.Email,
                TotalAmount = cart.TotalPrice
            };
            context.Orders.Add(newOrder);
            context.SaveChanges();


        }

        public List<OrderForListDto> GetAllOrderWithDetails()
        {
            using var context = new ModelDbContext();

            var orders = context.Orders.Include(o => o.Cart).Include(o => o.User).Include(o => o.Cart.CartItems);
            List<OrderForListDto> orderForListDtos = new List<OrderForListDto>();

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
