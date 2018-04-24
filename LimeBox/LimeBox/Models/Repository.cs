using LimeBox.Models.Entities;
using LimeBox.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static LimeBox.Models.ViewModels.AccountSettingsVM;

namespace LimeBox.Models
{
    public class Repository : IRepository
    {
        public LimeContext context;
        private readonly UserManager<IdentityUser> userManager;

        public Repository(LimeContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public List<BoughtBoxesVM> GetBoughtBoxesVM(string userName)
        {
            var aspUser = userManager.FindByNameAsync(userName).Result;
            var currentUser = context.Users.Single(u => u.AspNetId == aspUser.Id);
            List<BoughtBoxesVM> list = new List<BoughtBoxesVM>();

            List<Orders> orderList = GetAllOrdersById(currentUser.Id);

            foreach (var item in orderList)
            {
                list.Add(new BoughtBoxesVM
                {
                    Orders = item,
                    Boxes = GetBoxesByOrderId(item.Id)
                });
            }

            return list;
        }

        private List<Boxes> GetBoxesByOrderId(int id)
        {
            List<Boxes> boxes = new List<Boxes>();

            var foo = context.OrderRows.Where(r => r.OrderId == id).Select(o => o.BoxId).ToArray();

            foreach (var boxId in foo)
            {
                boxes.Add(FindBoxById(boxId));
            }

            return boxes;
        }

        public void ChangeOrderStatus(int id, int status)
        {
            var order = context.Orders.Find(id);
            order.Status = status;
            context.SaveChanges();
        }

        public OrderVM GetOrderVM(int id)
        {

            var order = context.Orders.Find(id);
            Status foo = (Status)Enum.ToObject(typeof(Status), order.Status);

            return new OrderVM
            {
                Order = order,
                Boxes = GetBoxesByOrderId(id),
                StatusName = foo.ToString(),
                StatusNameList = GetListOfOptions()
            };
        }

        private List<string> GetListOfOptions()
        {
            List<string> list = new List<string>();

            var foo = Enum.GetValues(typeof(Status));

            foreach (var item in foo)
            {
                list.Add(item.ToString());
            }

            return list; 
        }

        public List<AdminAllOrdersVM> GetAdminAllOrdersVM()
        {
            List<AdminAllOrdersVM> orders = new List<AdminAllOrdersVM>();

            var allOrders = context.Orders.OrderByDescending(o => o.Id).ToList();

            foreach (var item in allOrders)
            {
                Status status = (Status)Enum.ToObject(typeof(Status), item.Status);
                orders.Add(new AdminAllOrdersVM
                {
                    Order = item,
                    StatusName = status.ToString(),
                });
            }
            

            return orders;
        }


        private List<Orders> GetAllOrdersById(int id)
        {
            var order = context.Orders.Where(o => o.UserId == id).ToList();

            return order;
        }

        public void GenerateBoxes(int boxTypeId, decimal price)
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
                    BoxId = i,
                    BoxTypeId = boxTypeId,
                    BoxValue = valueNumber,
                    BoxPrice = price,
                    Bought = false,
                });
            }
            context.SaveChanges();
        }

        public int CreateBoxType(string boxType, string ImageUrl, string ImageUrlHeader, string description)
        {
            BoxTypes box = new BoxTypes
            {
                BoxType = boxType,
                BoxImage = ImageUrl,
                BoxImageHeader = ImageUrlHeader,
                BoxDescription = description
            };
            context.BoxTypes.Add(box);
            context.SaveChanges();
            return box.Id;
        }

        public async Task<HomeCheckoutVM> GetHomeCheckoutVM(ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                var LoggedInUser = userManager.GetUserAsync(user).Result;
                var currentUser = await context.Users.SingleAsync(u => u.AspNetId == LoggedInUser.Id);
                return new HomeCheckoutVM
                {
                    Boxes = ShoppingCart.GetCart(),
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Address = currentUser.Address,
                    City = currentUser.City,
                    PostalCode = currentUser.PostalCode,
                    Sum = ShoppingCart.SumCart(),
                    Email = LoggedInUser.Email,
                    PhoneNumber = LoggedInUser.PhoneNumber
                };
            }
            return new HomeCheckoutVM
            {
                Boxes = ShoppingCart.GetCart(),
                Sum = ShoppingCart.SumCart()
            };
        }

        public AccountOrderVM GetAccountOrderVM(int id)
        {
            return new AccountOrderVM
            {
                Order = GetOrderById(id),
                Boxes = GetBoxesByOrderId(id)
            };
        }

        private Orders GetOrderById(int id)
        {
            return context.Orders.Find(id);
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

        public async Task<CreateFormVM> GetAccountSettingsVM(ClaimsPrincipal user)
        {
            var LoggedInUser = userManager.GetUserAsync(user).Result;
            var currentUser = await context.Users.SingleAsync(u => u.AspNetId == LoggedInUser.Id);
            return new CreateFormVM
            {
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Address = currentUser.Address,
                City = currentUser.City,
                PostalCode = (int)currentUser.PostalCode,
                Email = LoggedInUser.Email,
                PhoneNumber = LoggedInUser.PhoneNumber,
                Username = LoggedInUser.UserName
            };
        }

        public void CreateOrder(HomeCheckoutVM model, ClaimsPrincipal user)
        {
            string currentUserAspId;
            Users currentUser;
            if (user.Identity.IsAuthenticated)
            {
                currentUserAspId = userManager.GetUserId(user);
                currentUser = context.Users.Single(u => u.AspNetId == currentUserAspId);
            }
            else
                currentUser = new Users { Id = -1 };

            var cart = ShoppingCart.GetCart();
            Orders order = new Orders
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                City = model.City,
                PostalCode = model.PostalCode.Value,
                Status = 1,
                OrderDate = DateTime.Now
            };
            if (user.Identity.IsAuthenticated)
                order.UserId = currentUser.Id;

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
            cart.Clear();
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
                BoxImageHeader = boxType.BoxImageHeader,

                Items = context.Boxes

            .Where(b => b.BoxTypeId == Id && b.Bought == false)
              //vi vill bara ha boxar som har det id:et
              .Select(s => new ManyBoxesItemVM
              {
                  BoxId = s.BoxId,
                  BoxTypeName = s.BoxType.BoxType,
                  BoxImg = s.BoxType.BoxImage,
                  Id = s.Id,
                  Price = s.BoxPrice
              }).OrderBy(o => o.BoxId).ToArray()
            };

        }
    }
}
