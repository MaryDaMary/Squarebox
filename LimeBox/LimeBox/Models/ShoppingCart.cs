using LimeBox.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models
{
    public class ShoppingCart
    {
        static List<Boxes> cart = new List<Boxes>();

        static public void AddToCart(Boxes box)
        {
            if (!ExistInList(box.Id))
                cart.Add(box);
        }

        public static bool ExistInList(int id)
        {
            foreach (var item in cart)
            {
                if (item.Id == id)
                    return true;
            }
            return false;
        }

        static public void RemoveFromCart(Boxes box)
        {
            cart.RemoveAll(b => b.Id == box.Id);
        }

        static public List<Boxes> GetCart()
        {
            return cart;
        }

        internal static bool IsEmpty()
        {
            if (cart.Count <= 0)
                return true;
            else
                return false;
        }
    }
}
