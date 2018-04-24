using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LimeBox.Models.Entities;
using LimeBox.Models.ViewModels;

namespace LimeBox.Models
{
    public class TestRepository : IRepository
    {

        private List<AdminAllOrdersVM> adminAllOrdersVMs;

        static List<BoxTypes> BoxTypes = new List<BoxTypes>()
        {
            new BoxTypes {Id = 1, BoxType = "Limebox", BoxDescription = "Box Info", BoxImage = "image", BoxImageHeader = "image"},
            new BoxTypes {Id = 1, BoxType = "Skönhetsboxen", BoxDescription = "Box Info", BoxImage = "image", BoxImageHeader = "image"},
            new BoxTypes {Id = 1, BoxType = "Syndarboxen", BoxDescription = "Box Info", BoxImage = "image", BoxImageHeader = "image"}
        };

        static List<Boxes> Boxes = new List<Boxes>()
        {
            new Boxes{ Id = 1, Bought = false, BoxId = 1, BoxPrice = 199, BoxTypeId = 1, BoxValue = 1 },
            new Boxes{ Id = 2, Bought = true, BoxId = 2, BoxPrice = 199, BoxTypeId = 2, BoxValue = 2 },
            new Boxes{ Id = 3, Bought = false, BoxId = 3, BoxPrice = 199, BoxTypeId = 3, BoxValue = 1 },
            new Boxes{ Id = 4, Bought = true, BoxId = 4, BoxPrice = 199, BoxTypeId = 2, BoxValue = 3 },
            new Boxes{ Id = 5, Bought = true, BoxId = 5, BoxPrice = 199, BoxTypeId = 1, BoxValue = 2,}
        };

        static List<Orders> Orders = new List<Orders>()
        {
            new Orders{Id = 1, FirstName = "Test1", Address = "Adress1", City = "City1", Email = "Email1", LastName = "Test1", OrderDate = DateTime.Now, PhoneNumber = "01234567", PostalCode = 12345, Status = 1, UserId = null},
            new Orders{Id = 2, FirstName = "Test2", Address = "Adress2", City = "City2", Email = "Email2", LastName = "Test2", OrderDate = DateTime.Now, PhoneNumber = "01234567", PostalCode = 12345, Status = 2, UserId = 1},
            new Orders{Id = 3, FirstName = "Test3", Address = "Adress3", City = "City3", Email = "Email3", LastName = "Test3", OrderDate = DateTime.Now, PhoneNumber = "01234567", PostalCode = 12345, Status = 3, UserId = null},
            new Orders{Id = 4, FirstName = "Test4", Address = "Adress4", City = "City4", Email = "Email4", LastName = "Test4", OrderDate = DateTime.Now, PhoneNumber = "01234567", PostalCode = 12345, Status = 2, UserId = 2}
        };


        static List<OrderRows> OrderRows = new List<OrderRows>()
        {
            new OrderRows{ Id = 1, BoxId = 1, OrderId = 2},
            new OrderRows{ Id = 2, BoxId = 2, OrderId = 4},
            new OrderRows{ Id = 2, BoxId = 2, OrderId = 4}
        };

        static List<Users> Users = new List<Users>()
        {
            new Users{ Id = 1, FirstName = "Test1", LastName = "Test1", Address = "Adress1", City = "City1", PostalCode = 12345},
            new Users{ Id = 1, FirstName = "Test2", LastName = "Test2", Address = "Adress2", City = "City2", PostalCode = 12345}
        };

        public void ChangeOrderStatus(int id, int status)
        {
            
        }

        public int CreateBoxType(string boxType, string ImageUrl, string ImageUrlHeader, string description)
        {
            if (boxType == null || ImageUrl == null || ImageUrlHeader == null || description == null)
                throw new Exception();
            return 1;
        }

        public void CreateOrder(HomeCheckoutVM model, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public Boxes FindBoxById(int id)
        {
            throw new NotImplementedException();
        }

        public void GenerateBoxes(int boxTypeId, decimal price)
        {
 
        }

        public AccountOrderVM GetAccountOrderVM(int id)
        {
            return new AccountOrderVM { Boxes = Boxes, Order = Orders[0] };
        }

        public async Task<AccountSettingsVM.CreateFormVM> GetAccountSettingsVM(ClaimsPrincipal user)
        {
            return new AccountSettingsVM.CreateFormVM { Address = "test", City = "test", Email = "test", FirstName = "test", LastName = "test", PhoneNumber = "01234567", PostalCode = 12345, Username = "test" };
        }

        public List<AdminAllOrdersVM> GetAdminAllOrdersVM()
        {
            return adminAllOrdersVMs;
        }

        public void setAdminAllOrdersVm(List<AdminAllOrdersVM> list)
        {
            adminAllOrdersVMs = list;
        }

        public List<BoughtBoxesVM> GetBoughtBoxesVM(string userName)
        {
            throw new NotImplementedException();
        }

        public GetBoxDataBaseVM[] GetBoxesDataBase()
        {
            return new GetBoxDataBaseVM[] { new GetBoxDataBaseVM { Id = 1, Image = "Image", Name = "Limebox" }, new GetBoxDataBaseVM { Id = 2, Image = "image", Name = "syndarboxen"} };
        }

        public async Task<HomeCheckoutVM> GetHomeCheckoutVM(ClaimsPrincipal user)
        {
            return new HomeCheckoutVM { Address = "Test", Boxes = Boxes, City = "Test", Email = "Test@Test.com", FirstName = "Test", LastName = "Test", PhoneNumber = "01234567", PostalCode = 12345, Sum =  995 };
        }

        public ManyBoxesVM GetManyBoxesVM(int Id)
        {
            return new ManyBoxesVM(); 
        }

        public OrderVM GetOrderVM(int id)
        { 
            return new OrderVM { Order = Orders[0] };
        }
    }
}
