using Core.DataAccess;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICartDal : IRepositoryBase<Cart>
    {
        public List<Cart> GetAllCarts(Guid userId);
        public List<CartItemDto> GetAllCartItemsByUserId(Guid userId);
        public void AddItemToCart(Guid userId,Guid productId,int count);
        public void DeleteItemFromCart(CartItem item);
        public void DeleteAllCartItemAfterOrder(Guid userId);
        public CartDto GetCartByUserId(Guid userId);
        public void UpdateCartItem(CartItem cartItem);

    }
}
