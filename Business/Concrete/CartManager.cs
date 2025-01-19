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
        private readonly IMapper _mapper;

        public CartManager(ICartDal cartDal, IMapper mapper)
        {
            _cartDal = cartDal;
            _mapper = mapper;
        }

        public IResult Add()
        {
            var newCart = new Cart()
            {
                UserId = "fb78b7e1-8851-411e-bd28-f33d16d15b39",
                CartItems = new List<CartItem>()
            };
            _cartDal.Add(newCart);
            return new SuccessResult("Sebet ugurla yaradildi");
        }

        public IResult AddCartWithUserId(string userId)
        {
            if(userId == null)
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

        public IResult AddItemToCart(int cartId, CartItemDto cartItemDto)
        {
            //if(cartItemDto == null)
            //{
            //    return new ErrorResult("Model bosdur.");
            //}
            //var cart = _cartDal.Get(c => c.Id == cartId);
            //if(cart != null)
            //{
            //    cart.CartItems.Add(cartItemDto);
            //}
            //cartItemDto.CartId = cart.Id;
            //_cartDal.AddItemToCart(cartItemDto);
            //return new SuccessResult("Mehsul ugurla sebete elave olundu");
            var cart = _cartDal.Get(c => c.Id == cartItemDto.CartId);
            if (cart == null)
            {
                return new ErrorResult("Sebet tapilmadi");
            }
            var cartItem = _mapper.Map<CartItem>(cartItemDto);
            if (cart.CartItems == null)
            {
                cart.CartItems = new List<CartItem>();
            }
            cart.CartItems.Add(cartItem);
            _cartDal.AddItemToCart(cartItem);
            return new SuccessResult("Mehsul ugurla elave olundu");
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

        public IDataResult<Cart> GetByUserId(string userId)
        {
            if(userId == null)
            {
                return new ErrorDataResult<Cart>("User ID bos ola bilmez");
            }
            return new SuccessDataResult<Cart>(_cartDal.GetCartByUserId(userId), "Sebet ugurla getirildi");
        }

        public IResult Update(int id, Cart? cart)
        {
            throw new NotImplementedException();
        }
    }
}
