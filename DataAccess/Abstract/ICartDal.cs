using Core.DataAccess;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICartDal : IRepositoryBase<Cart>
    {
        public void AddItemToCart(CartItem item);
        public void DeleteItemFromCart(CartItem item);
        public Cart GetCartByUserId(string userId);

    }
}
