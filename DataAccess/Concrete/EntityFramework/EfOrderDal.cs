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

        public void ChangeStatus(Guid id)
        {
            using var context = new ModelDbContext();

            var order = context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                throw new Exception("Order tapılmadı!");
            }

            order.IsSuccess = !order.IsSuccess;

            context.SaveChanges();
        }

        public void CreateOrder(OrderDto order, Guid userId)
        {
            using var context = new ModelDbContext();

            var cart = context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product) // Product-ları da daxil edirik
                .FirstOrDefault(c => c.UserId == userId);

            var user = context.Users.FirstOrDefault(u => u.Id == userId);

            if (cart == null || user == null)
            {
                throw new Exception("Cart və ya User tapılmadı!");
            }

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

            // 🛠 **Məhsulların `SaleCount` dəyərini artırırıq**
            foreach (var cartItem in cart.CartItems)
            {
                if (cartItem.Product != null)
                {
                    cartItem.Product.SaleCount += cartItem.Quantity;
                }
            }

            context.SaveChanges();
        }

        public List<OrderForListDto> GetAllNotSuccessOrders()
        {
            using var context = new ModelDbContext();
            var orders = context.Orders.Include(o => o.Cart).Include(o => o.User).Where(o => !o.IsSuccess).ToList();
            var dtos = new List<OrderForListDto>();
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
                dtos.Add(dto);
            }
            return dtos;
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

        public List<OrderForListDto> GetAllSuccessOrders()
        {
            using var context = new ModelDbContext();
            var orders = context.Orders.Include(o => o.Cart).Include(o => o.User).Where(o => o.IsSuccess).ToList();
            var dtos = new List<OrderForListDto>();
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
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}