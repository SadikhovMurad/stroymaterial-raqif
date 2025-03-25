using Core.DataAccess.EntityFramework;
using Core.Entity.Concrete;
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
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.UserId == userId && !c.IsOrdered);

            var user = context.Users.FirstOrDefault(u => u.Id == userId);

            if (cart == null || user == null)
            {
                throw new Exception("Cart və ya User tapılmadı!");
            }

            if (cart.IsOrdered)
            {
                throw new Exception("Cart artıq sifariş edilib!");
            }

            // **Sifariş obyektini yaradırıq**
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
                TotalAmount = cart.TotalPrice,
                OrderItems = new List<OrderItem>() // **Boş bir list yaradırıq**
            };

            context.Orders.Add(newOrder);
            context.SaveChanges(); // **Burada SaveChanges çağıraraq Order-in Id-sinin yaradılmasını təmin edirik!**

            // **OrderItems-ı yaradırıq**
            var orderItems = cart.CartItems.Select(ci => new OrderItem
            {
                OrderId = newOrder.Id, // **İndi Order-in ID-si var**
                ProductId = ci.ProductId,
                ProductImageUrl = ci.Product.ImageUrl,
                ProductName = ci.Product.Name,
                ProductPrice = ci.Product.Price,
                ItemTotalPrice = ci.ItemTotalPrice,
                Quantity = ci.Quantity,
            }).ToList();

            context.OrderItems.AddRange(orderItems); // **Bütün OrderItems-ı bazaya əlavə edirik**

            // **Sifariş edilən məhsulların `IsOrderedItem` dəyərini TRUE edirik**
            foreach (var cartItem in cart.CartItems)
            {
                cartItem.IsOrderedItem = true;  // Artıq sifariş edilib
                if (cartItem.Product != null)
                {
                    cartItem.Product.SaleCount += cartItem.Quantity;
                }
            }

            cart.IsOrdered = true; // Səbəti tamamilə sifariş edilmiş kimi işarələyirik
            cart.CartItems.Clear(); // Səbətin içini təmizləyirik

            context.SaveChanges(); // **Son olaraq bütün dəyişiklikləri bazaya yazırıq**
        }

        public List<OrderForListDto> GetAllNotSuccessOrders()
        {
            using var context = new ModelDbContext();
            var orders = context.Orders.Include(o => o.Cart).Include(o => o.OrderItems).Include(o=>o.User).Where(o => !o.IsSuccess).ToList();
            var dtos = new List<OrderForListDto>();



            foreach (var order in orders)
            {
                OrderForListDto dto = new OrderForListDto()
                {
                    OrderId = order.Id,
                    CartId = order.CartId,
                    OrderItems = order.OrderItems,
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

            var orders = context.Orders.Include(o=>o.Cart).Include(o=>o.OrderItems).Include(o => o.User);
            List<OrderForListDto> orderForListDtos = new List<OrderForListDto>();
            foreach (var order in orders)
            {
                OrderForListDto dto = new OrderForListDto()
                {
                    OrderId = order.Id,
                    CartId = order.CartId,
                    OrderItems = order.OrderItems,
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
            var orders = context.Orders.Include(o => o.Cart).Include(o => o.OrderItems).Include(o => o.User).Where(o => o.IsSuccess).ToList();
            var dtos = new List<OrderForListDto>();



            foreach (var order in orders)
            {
                OrderForListDto dto = new OrderForListDto()
                {
                    OrderId = order.Id,
                    CartId = order.CartId,
                    OrderItems = order.OrderItems,
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

        public OrderForListDto GetOrderById(Guid userId)
        {
            using var context = new ModelDbContext();

            var order = context.Orders
                .Include(o=>o.Cart)
                .Include(o => o.OrderItems)
                .Include(o => o.User)
                .FirstOrDefault(o => o.UserId == userId);

            if (order == null)
            {
                return null; // Əgər sifariş yoxdursa, null qaytar
            }


            // Burada düzgün obyektin qaytarılması lazımdır
            return new OrderForListDto()
            {
                OrderId = order.Id,
                CartId = order.CartId,
                OrderItems = order.OrderItems,
                OrderDate = order.OrderDate,
                Location = order.Location,
                UserEmail = order.User.Email,
                UserName = order.User.Firstname,
                UserPhoneNumber = order.PhoneNumber,
                Notification = order.Notification,
                TotalAmount = order.TotalAmount,
                Success = order.IsSuccess
            };
        }
    }
}