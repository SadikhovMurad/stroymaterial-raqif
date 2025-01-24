using Core.DataAccess.EntityFramework;
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
        public void AddItemToCart(CartItem item)
        {
            using var context = new ModelDbContext();
            context.CartItems.Add(item);
            context.SaveChanges();
        }

        public void DeleteItemFromCart(CartItem item)
        {
            using var context = new ModelDbContext();
            context.CartItems.Remove(item);
            context.SaveChanges();
        }

        public List<CartItemDto> GetAllCartItemsByUserId(string userId)
        {
            using var context = new ModelDbContext();

            var carts = context.Carts.Include(x => x.CartItems).Where(x=>x.UserId == userId).ToList();
            List<CartItemDto> cartItemDtos = new List<CartItemDto>();
            
            foreach (var item in carts)
            {
                foreach(var cartItem in item.CartItems)
                {
                    CartItemDto dto = new CartItemDto
                    {
                        CartId = cartItem.Id,
                        ProductId = cartItem.ProductId,
                        UserName = "Admin",
                        UserEmail = "Admin@gmail.com",
                        Quantity = cartItem.Quantity,
                        ItemTotalPrice = cartItem.ItemTotalPrice
                    };
                    cartItemDtos.Add(dto);
                }
            }
            return cartItemDtos;
        }

        public List<Cart> GetAllCarts(string userId)
        {
            using var context = new ModelDbContext();

            var carts = context.Carts.Include(x => x.CartItems).Where(c=>c.UserId == userId).ToList();
            return carts;
        }

        public Cart GetCartByUserId(string userId)
        {
            using var context = new ModelDbContext();
            var cart = context.Carts
           .Include(c => c.CartItems)
           .ThenInclude(ci => ci.Product)
           .FirstOrDefault(c => c.UserId == userId);

            return cart;
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
