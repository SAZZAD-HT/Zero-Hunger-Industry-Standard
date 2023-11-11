using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Services.Description;
using Zero_Hunger_Industry_Standard.Dtos;

namespace Zero_Hunger_Industry_Standard
{
    public interface IRestrurantService
    {

      Task<RestrurantDto> GetRestrurantDetails(long RestrurantId);
      Task<ResponseMessgeDto> CreateUpdateDeleteRestruran(RestrurantDto Restrurant);
      Task<ResponseMessgeDto> OpenCollectionRequestForm(RestrurantDto Restrurant);
      Task<List<RestrurantFoodDto>> GetCollectionRequest(DateTime? Date);
      Task<ResponseMessgeDto> TakeFood(RestrurantFoodDto EmployeeID);
      Task<ResponseMessgeDto> CreateUser(UserDto user);
      Task<List<UserDto>> ViewUsers(long? UserID );
    }
}
