﻿using Core.DataAccess.EntityFramework;
using Core.Entity.Abstract;
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
    public class EfCartDal : EfRepositoryBase<Cart, ModelDbContext>, ICartDal
    {
        public void AddItemToCart(Guid userId, Guid productId, int count = 1)
        {
            using var context = new ModelDbContext();

            var cart = context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.UserId == userId);

            var product = context.Products.FirstOrDefault(p => p.Id == productId);

            if (cart == null)
            {
                throw new Exception("Cart not found");
            }

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            // Mövcud məhsul cart-da varsa, sayını artır və qiymətini yenilə
            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingCartItem != null)
            {
                if (count > 1)
                {
                    existingCartItem.Quantity += count;
                }
                else
                {
                    existingCartItem.Quantity++;
                }

                existingCartItem.ItemTotalPrice = (decimal)existingCartItem.Product.Price * existingCartItem.Quantity;
            }
            else
            {
                // Əgər məhsul cart-da yoxdursa, yeni item əlavə et
                var newCartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = product.Id,
                    Quantity = 1,
                    IsOrderedItem = false,
                    ItemTotalPrice = product.Price
                };

                context.CartItems.Add(newCartItem);
            }

            cart.TotalPrice = cart.CartItems.Sum(ci => ci.ItemTotalPrice);
            cart.IsOrdered = false;

            // Dəyişiklikləri yadda saxla
            context.SaveChanges();

        }

        public void DeleteAllCartItemAfterOrder(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteItemFromCart(CartItem item)
        {
            using var context = new ModelDbContext();
            context.CartItems.Remove(item);
            context.SaveChanges();
        }

        public void DeleteItemFromCartId(int id)
        {
            using var context = new ModelDbContext();
            var item = context.CartItems.FirstOrDefault(ci => ci.Id == id);
            context.CartItems.Remove(item);
            context.SaveChanges();
        }

        public List<CartItemDto> GetAllCartItemsByUserId(Guid userId)
        {
            using var context = new ModelDbContext();

            var carts = context.Carts
                .Include(x => x.CartItems)
                .Where(x => x.UserId == userId && !x.IsOrdered) // Sifariş verilməmiş səbətləri götür
                .ToList();

            List<CartItemDto> cartItemDtos = new List<CartItemDto>();
            var user = context.Users.FirstOrDefault(u => u.Id == userId);

            foreach (var item in carts)
            {
                foreach (var cartItem in item.CartItems)
                {
                    if (!cartItem.IsOrderedItem) // **Sifariş edilmiş məhsulları filtr et**
                    {
                        var product = context.Products.FirstOrDefault(p => p.Id == cartItem.ProductId);
                        CartItemDto dto = new CartItemDto
                        {
                            CartId = cartItem.Id,
                            ProductId = cartItem.ProductId,
                            FirstName = user.Firstname,
                            LastName = user.Lastname,
                            Email = user.Email,
                            ProductName = product.Name,
                            ProductPrice = product.Price,
                            ImageUrl = product.ImageUrl,
                            IsOrderedItem = cartItem.IsOrderedItem,
                            Quantity = cartItem.Quantity,
                            ItemTotalPrice = cartItem.ItemTotalPrice,
                        };
                        cartItemDtos.Add(dto);
                    }
                }
            }
            return cartItemDtos;
        }

        public List<Cart> GetAllCarts(Guid userId)
        {
            using var context = new ModelDbContext();

            var carts = context.Carts.Include(x => x.CartItems).Where(c => c.UserId == userId).ToList();
            return carts;
        }

        public CartDto GetCartByUserId(Guid userId)
        {
            using var context = new ModelDbContext();
            var cart = context.Carts
           .Include(c => c.CartItems)
           .ThenInclude(ci => ci.Product)
           .FirstOrDefault(c => c.UserId == userId);
            var user = context.Users.FirstOrDefault(u => u.Id == cart.UserId);

            if (cart.IsOrdered == true)
            {

                var dto = new CartDto()
                {
                    UserId = user.Id,
                    FirstName = user.Firstname,
                    LastName = user.Lastname,
                    email = user.Email,
                    PhoneNumber = user.Email,
                    TotalPrice = cart.CartItems.Sum(c => c.ItemTotalPrice),
                    isOrdered = cart.IsOrdered,
                    cartItemsIsNull = true,
                    CartItems = null,
                };
                return dto;
            }
            else
            {

                var dto = new CartDto()
                {
                    UserId = user.Id,
                    FirstName = user.Firstname,
                    LastName = user.Lastname,
                    email = user.Email,
                    PhoneNumber = user.Email,
                    TotalPrice = cart.CartItems.Sum(c => c.ItemTotalPrice),
                    isOrdered = cart.IsOrdered,
                    cartItemsIsNull = false,
                    CartItems = cart.CartItems.Select(ci => new CartAndCartItemDto
                    {
                        CartItemId = ci.Id,
                        ProductId = ci.ProductId,
                        ImageUrl = ci.Product.ImageUrl,
                        ProductPrice = ci.Product.Price,
                        ProductName = ci.Product.Name,
                        Quantity = ci.Quantity,
                        ItemTotalPrice = ci.ItemTotalPrice
                    }).ToList()
                };
                return dto;
            }

        }

        public void UpdateCartItem(CartItem cartItem)
        {
            using var context = new ModelDbContext();
            var updateEntity = context.Entry(cartItem);
            updateEntity.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
