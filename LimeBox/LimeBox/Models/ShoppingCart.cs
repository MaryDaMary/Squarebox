using LimeBox.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models
{
    public class ShoppingCart
    {
        static List<BoxType> cart = new List<BoxType>();

        static public void AddToCart(BoxType box)
        {
            cart.Add(box);
        }

        static public void RemoveFromCart(BoxType box)
        {
            cart.RemoveAll(b => b.Id == box.Id);
        }

        static public List<BoxType> GetCart()
        {
            return cart;
        }
    }
}
