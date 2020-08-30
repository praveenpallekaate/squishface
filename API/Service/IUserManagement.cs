using SquishFaceAPI.Model.View;
using System.Collections.Generic;

namespace SquishFaceAPI.Service
{
    public interface IUserManagement
    {
        string LoginUser(UserDetail item);
        string ResetUser(UserDetail item);
        string AddUser(UserDetail item);
        string EditUser(UserDetail item);
        void DeleteUser(string id);
        IEnumerable<UserDetail> GetAllUsers();
        string CheckExists(UserDetail userDetail);
        IEnumerable<UserDetail> GetActiveUsers();
        string[] GetAllUserName();
        void ResetGlobals();
    }
}
