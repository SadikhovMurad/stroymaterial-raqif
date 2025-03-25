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
                .FirstOrDefault(c => c.UserId == userId && !c.IsOrdered); // Sifariş verilməmiş səbəti tapırıq

            var user = context.Users.FirstOrDefault(u => u.Id == userId);

            if (cart == null || user == null)
            {
                throw new Exception("Cart və ya User tapılmadı!");
            }

            if (cart.IsOrdered)
            {
                throw new Exception("Cart'");
            }

            // Sifariş obyektini yaradırıq
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
            context.SaveChanges();
        }

        public List<OrderForListDto> GetAllNotSuccessOrders()
        {
            using var context = new ModelDbContext();
            var orders = context.Orders.Include(o => o.Cart).Include(o => o.User).Where(o => !o.IsSuccess).ToList();
            var dtos = new List<OrderForListDto>();
            var cartItemListDtos = new List<CartAndCartItemDto>();



            foreach (var order in orders)
            {
                foreach (var cartItem in order.Cart.CartItems)
                {
                    if (cartItem.IsOrderedItem && cartItem.CartId == order.CartId)
                    {
                        cartItemListDtos.Add(new CartAndCartItemDto
                        {
                            ProductId = cartItem.ProductId,
                            ProductName = cartItem.Product.Name,
                            ProductPrice = cartItem.Product.Price,
                            ItemTotalPrice = cartItem.ItemTotalPrice,
                            Quantity = cartItem.Quantity,
                            ImageUrl = cartItem.Product.ImageUrl
                        });
                    }
                }
                OrderForListDto dto = new OrderForListDto()
                {
                    OrderId = order.Id,
                    CartId = order.CartId,
                    CartItemDtos = cartItemListDtos,
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

            var orders = context.Orders.Include(o => o.Cart).ThenInclude(c => c.CartItems).ThenInclude(ci => ci.Product).Include(o => o.User);
            List<OrderForListDto> orderForListDtos = new List<OrderForListDto>();
            var cartItemListDtos = new List<CartAndCartItemDto>();
            foreach (var order in orders)
            {
                foreach (var cartItem in order.Cart.CartItems)
                {
                    if (cartItem.IsOrderedItem && cartItem.CartId == order.CartId)
                    {
                        cartItemListDtos.Add(new CartAndCartItemDto
                        {
                            ProductId = cartItem.ProductId,
                            ProductName = cartItem.Product.Name,
                            ProductPrice = cartItem.Product.Price,
                            ItemTotalPrice = cartItem.ItemTotalPrice,
                            Quantity = cartItem.Quantity,
                            ImageUrl = cartItem.Product.ImageUrl
                        });
                    }
                }
                OrderForListDto dto = new OrderForListDto()
                {
                    OrderId = order.Id,
                    CartId = order.CartId,
                    CartItemDtos = cartItemListDtos,
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
            var cartItemListDtos = new List<CartAndCartItemDto>();



            foreach (var order in orders)
            {
                foreach (var cartItem in order.Cart.CartItems)
                {
                    if (cartItem.IsOrderedItem && cartItem.CartId == order.CartId)
                    {
                        cartItemListDtos.Add(new CartAndCartItemDto
                        {
                            ProductId = cartItem.ProductId,
                            ProductName = cartItem.Product.Name,
                            ProductPrice = cartItem.Product.Price,
                            ItemTotalPrice = cartItem.ItemTotalPrice,
                            Quantity = cartItem.Quantity,
                            ImageUrl = cartItem.Product.ImageUrl
                        });
                    }
                }
                OrderForListDto dto = new OrderForListDto()
                {
                    OrderId = order.Id,
                    CartId = order.CartId,
                    CartItemDtos = cartItemListDtos,
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
                .Include(o => o.Cart)
                    .ThenInclude(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                .Include(o => o.User)
                .FirstOrDefault(o => o.UserId == userId);

            if (order == null)
            {
                return null; // Əgər sifariş yoxdursa, null qaytar
            }

            var cartItemListDtos = new List<CartAndCartItemDto>();

            foreach (var cartItem in order.Cart.CartItems)
            {
                if (cartItem.IsOrderedItem && cartItem.CartId == order.CartId)
                {
                    cartItemListDtos.Add(new CartAndCartItemDto
                    {
                        ProductId = cartItem.ProductId,
                        ProductName = cartItem.Product.Name,
                        ProductPrice = cartItem.Product.Price,
                        ItemTotalPrice = cartItem.ItemTotalPrice,
                        Quantity = cartItem.Quantity,
                        ImageUrl = cartItem.Product.ImageUrl
                    });
                }
            }

            // Burada düzgün obyektin qaytarılması lazımdır
            return new OrderForListDto()
            {
                OrderId = order.Id,
                CartId = order.CartId,
                CartItemDtos = cartItemListDtos,
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