using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
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

        public Cart GetCartByUserId(string userId)
        {
            using var context = new ModelDbContext();
            var cart = context.Carts
           .Include(c => c.CartItems)
           .ThenInclude(ci => ci.Product)
           .FirstOrDefault(c => c.UserId == userId);

            return cart;
        }
    }
}
