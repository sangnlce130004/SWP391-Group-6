using BikeShop.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BikeShop.Extensions
{
    public class State
    {
        HttpContext HttpContext;

        public State(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }

        public void AddSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        public string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }

        public void RemoveSession(string key)
        {
            HttpContext.Session.Remove(key);
        } 

        public bool IsAuthenticated()
        {
            return GetSession("loginKey") != null;
        }

        public string LoginUserName()
        {
            return GetSession("loginKey");
        }

        public void Logout()
        {
            RemoveSession("loginKey");
        }

        public Cart LoadCart()
        {
            Cart cart;
            string cartText = GetSession("cart");
            if (string.IsNullOrEmpty(cartText))
            {
                cart = new Cart();
            }
            else
            {
                cart = Cart.Deserialize(cartText);
            }

            return cart;
        }

        public void SaveCart(Cart cart)
        {
            if(cart != null)
            {
                string cartText = cart.Serialize();
                AddSession("cart", cartText);
            }
        }

    }
}
