using LimeBox.Models.Entities;
using LimeBox.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models
{
    public class Repository
    {
        public LimeContext context;

        public Repository(LimeContext context)
        {
            this.context = context;
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
                if (numberIsInArray(random5, i))
                    valueNumber = 3;
                else if (numberIsInArray(random15, i))
                    valueNumber = 2;
                context.Add(new BoxType
                {
                    BoxId = i,
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

                } while (numberIsInArray(numbers, randomNumber));

                numbers[i] = randomNumber;
            }

            return numbers;
        }

        internal void CreateOrder(HomeCheckoutVM model)
        {
            var cart = ShoppingCart.GetCart();
            Orders order = new Orders
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                City = model.City,
                PostalCode = model.PostalCode,
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

        private void ItemIsBought(BoxType item)
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

                } while (numberIsInArray(numbers, randomNumber) || numberIsInArray(array, randomNumber));

                numbers[i] = randomNumber;
            }

            return numbers;
        }

        private bool numberIsInArray(int[] numbers, int number)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == number)
                    return true;
            }
            return false;
        }

        public BoxType FindBoxById(int id)
        {
            return context.Boxes.Find(id);
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

        public ManyBoxesVM[] GetManyBoxesVM(int Id)
        {
            var boxes = context.Boxes
            .Where(b => b.BoxTypeId == Id)
            //vi vill bara ha boxar som har det id:et
              .Select(s => new ManyBoxesVM
              {
                  BoxId = s.BoxId,
                  BoxTypeName = s.BoxTypes.BoxType,
                  BoxImg = s.BoxTypes.BoxImage

              });
            

            return boxes.ToArray();
        }

        //public GetBoxDataBaseVM[] GetBoxesScroll()
        //{
        //    var boxTypes = context.BoxTypes
        //        .Select(s => new GetBoxDataBaseVM
        //        {
        //            Id = s.Id,
        //            Name = s.BoxType,
        //            Image = s.BoxImage
        //        });

        //    return boxTypes.ToArray();
        //}
    }
}
