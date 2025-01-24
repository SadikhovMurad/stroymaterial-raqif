﻿using Core.Utilities.Results;
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
        IDataResult<Cart> GetByUserId(string userId);
        IDataResult<List<CartItemDto>> GetAllCartItemsByUserId(string userId);
        IResult Add();
        IResult AddCartWithUserId(string userId);
        IResult Update(int id, Cart? cart);
        IResult Delete(int id);
        IResult AddItemToCart(string userId,Guid productId);
        IResult DeleteItemFromCart(int cartId,CartItem cartItem);
    }
}
