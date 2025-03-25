using Core.Utilities.Results;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICartService
    {
        IDataResult<List<Cart>> GetAll();
        IDataResult<CartDto> GetByUserId(Guid userId);
        IDataResult<List<CartItemDto>> GetAllCartItemsByUserId(Guid userId);
        IResult Add(Cart cart);
        IResult AddCartWithUserId(Guid userId);
        IResult Update(int id, Cart? cart);
        IResult Delete(int id);
        IResult AddItemToCart(Guid userId,Guid productId,int count);
        IResult DeleteItemFromCart(int cartId);
    }
}
