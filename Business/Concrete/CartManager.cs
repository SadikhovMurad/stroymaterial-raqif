using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Entity.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CartManager : ICartService
    {
        private readonly ICartDal _cartDal;
        private readonly IProductDal _productDal;
        private readonly IMapper _mapper;

        public CartManager(ICartDal cartDal, IMapper mapper, IProductDal productDal)
        {
            _cartDal = cartDal;
            _mapper = mapper;
            _productDal = productDal;
        }

        public IResult Add(Cart cart)
        {
            _cartDal.Add(cart);
            return new SuccessResult("Sebet ugurla yaradildi");
        }

        public IResult AddCartWithUserId(Guid userId)
        {
            if (userId == null)
            {
                return new ErrorResult("User ID bos ola bilmez");
            }
            var newCart = new Cart()
            {
                UserId = userId,
                CartItems = new List<CartItem>()
            };
            _cartDal.Add(newCart);
            return new SuccessResult("Sebet ugurla yaradildi");
        }

        public IResult AddItemToCart(Guid userId, Guid productId, int count=1)
        {
            _cartDal.AddItemToCart(userId, productId,count);
            return new SuccessResult("Mehsul karta ugurla elave olundu tesekkurler");
        }

        public IResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IResult DeleteItemFromCart(int cartId, CartItem cartItem)
        {
            if (cartItem == null)
            {
                return new ErrorResult("Model bosdur.");
            }
            var cart = _cartDal.Get(c => c.Id == cartId);
            if (cart != null)
            {
                cart.CartItems.Remove(cartItem);
            }
            _cartDal.DeleteItemFromCart(cartItem);
            return new SuccessResult("Mehsul ugurla sebetden cixarildi");
        }

        public IDataResult<List<Cart>> GetAll()
        {
            return new SuccessDataResult<List<Cart>>(_cartDal.GetAll(), "Sebetler ugurla getirildi");
        }

        public IDataResult<List<CartItemDto>> GetAllCartItemsByUserId(Guid userId)
        {
            return new SuccessDataResult<List<CartItemDto>>(_cartDal.GetAllCartItemsByUserId(userId), "Sebetdeki mehsullar ugurla getirildi");
        }

        public IDataResult<CartDto> GetByUserId(Guid userId)
        {
            if (userId == null)
            {
                return new ErrorDataResult<CartDto>("User ID bos ola bilmez");
            }
            return new SuccessDataResult<CartDto>(_cartDal.GetCartByUserId(userId), "Sebet ugurla getirildi");
        }

        public IResult Update(int id, Cart? cart)
        {
            throw new NotImplementedException();
        }
    }
}
