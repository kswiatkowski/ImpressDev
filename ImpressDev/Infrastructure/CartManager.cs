using ImpressDev.DAL;
using ImpressDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImpressDev.Infrastructure
{
    public class CartManager
    {
        private ImpressDevContext db;
        private ISessionManager session;

        public CartManager(ISessionManager session, ImpressDevContext db)
        {
            this.db = db;
            this.session = session;
        }

        public List<CartItem> GetCart()
        {
            List<CartItem> cart;

            if (session.Get<List<CartItem>>(Consts.CartSessionKey) == null)
            {
                cart = new List<CartItem>();
            }
            else
            {
                cart = session.Get<List<CartItem>>(Consts.CartSessionKey) as List<CartItem>;
            }
            return cart;
        }

        public void AddToCart(int bookId)
        {
            var cart = GetCart();
            var cartItem = cart.Find(x => x.Book.BookId == bookId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                var newBook = db.Books.Where(x => x.BookId == bookId).SingleOrDefault();

                if (newBook != null)
                {
                    var newCartItem = new CartItem()
                    {
                        Book = newBook,
                        Quantity = 1,
                        Price = newBook.Price
                    };
                    cart.Add(newCartItem);
                }
                session.Set(Consts.CartSessionKey, cart);
            }
        }

        public int RemoveFromCart(int bookId)
        {
            var cart = GetCart();
            var cartItem = cart.Find(x => x.Book.BookId == bookId);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    return cartItem.Quantity;
                }
                else
                {
                    cart.Remove(cartItem);
                }
            }
            return 0;
        }

        public decimal GetCartPrice()
        {
            var cart = GetCart();
            return cart.Sum(x => (x.Quantity * x.Book.Price));
        }

        public int GetCartQuantity()
        {
            var cart = GetCart();
            int quantity = cart.Sum(x => x.Quantity);
            return quantity;
        }

        public Order CreateNewOrder(Order newOrder, string userId)
        {
            var cart = GetCart();
            newOrder.DateAdded = DateTime.Now;
            //newOrder.userId = userId
            db.Orders.Add(newOrder);

            if (newOrder.OrderItems == null)
            {
                newOrder.OrderItems = new List<OrderItem>();
            }

            decimal cartPrice = 0;

            foreach (var cartItem in cart)
            {
                var newOrderItem = new OrderItem()
                {
                    BookId = cartItem.Book.BookId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Book.Price
                };
                cartPrice += (cartItem.Quantity * cartItem.Book.Price);
                newOrder.OrderItems.Add(newOrderItem);
            }
            newOrder.Price = cartPrice;
            db.SaveChanges();

            return newOrder;
        }

        public void CleanCart()
        {
            session.Set<List<CartItem>>(Consts.CartSessionKey, null);
        }
    }
}