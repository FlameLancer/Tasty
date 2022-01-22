using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models
{
    public class SessionCart
    {
        private ISession session;
        public static SessionCart GetSessionCart(IServiceProvider services)
        {
            var result = new SessionCart();
            result.session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            return result;
        }

        public Cart GetCart(Shop shop)
        {
            var cartList = session?.GetJson<List<Cart>>("CartList");
            if (cartList == null) return new Cart { Shop = shop };
            Cart cart = cartList.Where(c => c.Shop.IdEquals(shop)).FirstOrDefault() ?? new Cart { Shop = shop };
            return cart;
        }

        public void SaveCart(Cart cart)
        {
            var cartList = session?.GetJson<List<Cart>>("CartList") ?? new List<Cart>();
            bool isInserted = false;
            for (int i = 0; i < cartList.Count; ++i)
            {
                if (cartList[i].Shop.IdEquals(cart.Shop))
                {
                    isInserted = true;
                    cartList[i] = cart;
                    break;
                }
            }
            if (!isInserted)
            {
                cartList.Add(cart);
            }

            session?.SetJson("CartList", cartList);
        }
        public void Clear()
        {
            session?.Remove("CartList");
        }
    }
}
