using MyEshop.Core.DTOs;
using MyEshop.DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Services.Interfaces
{
    public interface IUserServices
    {
        IEnumerable<Users> GetUsers(int pageId);
        int GetCount();
        void AddUser(CreateUserViewModel user);
        bool IsPhoneNumberExist(int id, string number);
        bool IsEmailExist(int id, string email);
        EditUserViewModel GetUserInfo(int id);
        void EditUser(int id, EditUserViewModel user, string currentPhoto);
        void DeleteUser(int id);
        IEnumerable<Users> FilterUsers(string email, string userName, int pageId);
        int GetCountOfFilteredUsers(string email, string userName);

    }
}
