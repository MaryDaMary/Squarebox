using LimeBox.Models.Entities;
using LimeBox.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LimeBox.Models
{
    public class Repository
    {
        public LimeContext context;
        private readonly UserManager<IdentityUser> userManager;

        public Repository(LimeContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public void GenerateBoxes(int boxTypeId, decimal price, string ImageUrl)
        {
            //av 100 boxar så är:
            //standard 80st
            //premium 15st
            //Lyx 5st
            int[] random5 = GenerateRandomNumbers(5);
            int[] random15 = GenerateRandomNumbers(15, random5);
            for (int i = 1; i <= 100; i++)
            {
                int valueNumber = 1;
                if (NumberIsInArray(random5, i))
                    valueNumber = 3;
                else if (NumberIsInArray(random15, i))
                    valueNumber = 2;
                context.Add(new Boxes
                {
                    Id = i,
                    BoxTypeId = boxTypeId,
                    BoxValue = valueNumber,
                    BoxPrice = price,

                });
            }
            context.SaveChanges();
        }

        internal int CreateBoxType(string boxType)
        {
            BoxTypes box = new BoxTypes { BoxType = boxType };
            context.BoxTypes.Add(box);
            context.SaveChanges();
            return box.Id;
        }

        internal async Task<HomeCheckoutVM> GetHomeCheckoutVM(ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                var currentUserAspId = userManager.GetUserId(user);
                var currentUser = await context.Users.SingleAsync(u => u.AspNetId == currentUserAspId);
                return new HomeCheckoutVM
                {
                    Boxes = ShoppingCart.GetCart(),
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Address = currentUser.Address,
                    City = currentUser.City,
                    PostalCode = currentUser.PostalCode
                };
            }
            return new HomeCheckoutVM
            {
                Boxes = ShoppingCart.GetCart()
            };
        }

        private int[] GenerateRandomNumbers(int amount)
        {
            Random random = new Random();
            int[] numbers = new int[amount];

            for (int i = 0; i < amount; i++)
            {
                int randomNumber;
                do
                {
                    randomNumber = random.Next(1, 100 + 1);

                } while (NumberIsInArray(numbers, randomNumber));

                numbers[i] = randomNumber;
            }

            return numbers;
        }

        internal void CreateOrder(HomeCheckoutVM model, ClaimsPrincipal user)
        {
            var currentUserAspId = userManager.GetUserId(user);
            var currentUser = context.Users.Single(u => u.AspNetId == currentUserAspId);
            var cart = ShoppingCart.GetCart();
            Orders order = new Orders
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber.Value,
                Address = model.Address,
                City = model.City,
                PostalCode = model.PostalCode.Value,
                UserId = currentUser.Id
            };
            context.Orders.Add(order);
            foreach (var item in cart)
            {
                OrderRows orderRow = new OrderRows
                {
                    BoxId = item.Id,
                    Order = order
                };
                ItemIsBought(item);
                context.OrderRows.Add(orderRow);
            }
            context.SaveChanges();
        }

        private void ItemIsBought(Boxes item)
        {
            var box = FindBoxById(item.Id);
            box.Bought = true;

            context.SaveChanges();
        }

        private int[] GenerateRandomNumbers(int amount, int[] array)
        {
            Random random = new Random();
            int[] numbers = new int[amount];

            for (int i = 0; i < amount; i++)
            {
                int randomNumber;
                do
                {
                    randomNumber = random.Next(1, 100 + 1);

                } while (NumberIsInArray(numbers, randomNumber) || NumberIsInArray(array, randomNumber));

                numbers[i] = randomNumber;
            }

            return numbers;
        }

        private bool NumberIsInArray(int[] numbers, int number)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == number)
                    return true;
            }
            return false;
        }

        public Boxes FindBoxById(int id)
        {
            var box = context.Boxes.Find(id);
            var boxType = context.BoxTypes.Find(box.BoxTypeId);
            box.BoxType = boxType;
            return box;
        }


        public GetBoxDataBaseVM[] GetBoxesDataBase()
        {
            var boxTypes = context.BoxTypes
                .Select(s => new GetBoxDataBaseVM
                {
                    Id = s.Id,
                    Name = s.BoxType,
                    Image = s.BoxImage
                });

            return boxTypes.ToArray();
        }

        public ManyBoxesVM GetManyBoxesVM(int Id)
        {
            var boxType = context.BoxTypes.Find(Id);
            return new ManyBoxesVM
            {

                BoxDescription = boxType.BoxDescription,
                Boxtype = boxType.BoxType,
                BoxImage = boxType.BoxImage,

                Items = context.Boxes

            .Where(b => b.BoxTypeId == Id && b.Bought == false)
              //vi vill bara ha boxar som har det id:et
              .Select(s => new ManyBoxesItemVM
              {
                  BoxId = s.BoxId,
                  BoxTypeName = s.BoxType.BoxType,
                  BoxImg = s.BoxType.BoxImage,
                  Id = s.Id
              }).OrderBy(o => o.BoxId).ToArray()
            };

        }
    }
}
