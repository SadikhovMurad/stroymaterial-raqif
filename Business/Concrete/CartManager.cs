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

        public IResult AddItemToCart(string userId, Guid productId)
        {
            var cart = _cartDal.GetCartByUserId(userId);
            var product = _productDal.Get(c => c.Id == productId);
            if (cart == null)
            {
                return new ErrorResult("Bele bir sebet yoxdur");
            }

            if(cart.CartItems.Count == 0)
            {
                cart.CartItems.Add(
                    new CartItem()
                    {
                        CartId = cart.Id,
                        ProductId = product.Id,
                        Cart = cart,
                        Product = product,
                        Quantity = 1,
                        ItemTotalPrice = 0
                    });
                return new SuccessResult("Okay");
            }
            else
            {
                foreach (var item in cart.CartItems)
                {
                    if(item.ProductId == product.Id)
                    {
                        item.Quantity++;
                        _cartDal.UpdateCartItem(item);
                    }
                }
                return new SuccessResult("Added");
            }
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

        public IDataResult<List<CartItemDto>> GetAllCartItemsByUserId(string userId)
        {
            return new SuccessDataResult<List<CartItemDto>>(_cartDal.GetAllCartItemsByUserId(userId), "Sebetdeki mehsullar ugurla getirildi");
        }

        public IDataResult<Cart> GetByUserId(string userId)
        {
            if (userId == null)
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
