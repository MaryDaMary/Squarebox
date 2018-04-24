using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using LimeBox.Models.Entities;
using LimeBox.Models.ViewModels;

namespace LimeBox.Models
{
    public interface IRepository
    {
        void ChangeOrderStatus(int id, int status);
        int CreateBoxType(string boxType, string ImageUrl, string ImageUrlHeader, string description);
        void CreateOrder(HomeCheckoutVM model, ClaimsPrincipal user);
        Boxes FindBoxById(int id);
        void GenerateBoxes(int boxTypeId, decimal price);
        AccountOrderVM GetAccountOrderVM(int id);
        Task<AccountSettingsVM.CreateFormVM> GetAccountSettingsVM(ClaimsPrincipal user);
        List<AdminAllOrdersVM> GetAdminAllOrdersVM();
        List<BoughtBoxesVM> GetBoughtBoxesVM(string userName);
        GetBoxDataBaseVM[] GetBoxesDataBase();
        Task<HomeCheckoutVM> GetHomeCheckoutVM(ClaimsPrincipal user);
        ManyBoxesVM GetManyBoxesVM(int Id);
        OrderVM GetOrderVM(int id);
    }
}