using Microsoft.Extensions.Options;
using SquishFaceAPI.Common;
using SquishFaceAPI.Model;
using SquishFaceAPI.Model.Data;
using SquishFaceAPI.Model.View;
using SquishFaceAPI.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace SquishFaceAPI.Service
{
    public class UserManagement : IUserManagement
    {
        private readonly IRepository<User> _userRepository = null;
        private IEnumerable<UserDetail> users = null;

        public UserManagement(IOptions<AppConfig> appConfigs)
        {
            _userRepository = new UserRepository(appConfigs);
        }

        public string AddUser(UserDetail item)
        {
            Guid userId = Guid.NewGuid();

            _userRepository.CreateItem(new User
            {
                Email = item.Email,
                Name = item.Name,
                Password = item.Password,
                IsReset = false,
                Id = userId,                
                IsActive = false
            });

            ResetGlobals();

            return userId.ToString();
        }

        public string CheckExists(UserDetail userDetail)
        {
            var user = GetUsers()?
                .FirstOrDefault(i => 
                    i.Email.ToLower() == userDetail.Email.ToLower() ||
                    i.Name.ToLower() == userDetail.Name.ToLower());

            return user?.Id.ToString();
        }

        public void DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public string EditUser(UserDetail item)
        {
            var user = _userRepository.GetItem(i => i.Id == item.Id);
            user.ResetKey = item.ResetKey;
            user.IsReset = item.IsReset;
            user.Password = item.Password;

            _userRepository.UpdateItem(j => j.Id == item.Id, user);

            ResetGlobals();

            return item.Id.ToString();
        }

        public IEnumerable<UserDetail> GetActiveUsers()
        {
            return GetUsers()?.Where(i => i.IsActive);
        }

        public string[] GetAllUserName()
        {
            return GetUsers()?.Select(i => i.Name).ToArray();
        }

        public IEnumerable<UserDetail> GetAllUsers()
        {
            return _userRepository.GetAllItems()
                .Select(i =>
                {
                    return new UserDetail
                    {
                        Email = i.Email,
                        Name = i.Name,
                        IsReset = i.IsReset,
                        ResetKey = i.ResetKey,
                        Id = i.Id,
                        IsActive = i.IsActive
                    };
                });
        }

        public string LoginUser(UserDetail item)
        {
            string result = string.Empty;

            var user = _userRepository.GetItem(j => j.Name.ToLower() == item.Name.ToLower());
            
            if(user != null)
            {
                if (user.Password == item.Password)
                {
                    result = Constants.Valid;
                }
            }            

            return result;
        }

        public void ResetGlobals()
        {
            users = null;
        }

        public string ResetUser(UserDetail item)
        {
            var user = GetUsers()?
                .FirstOrDefault(i => i.Email.ToLower() == item.Email || i.Name.ToLower() == item.Name);

            item.Id = user.Id;

            return EditUser(item);
        }

        private IEnumerable<UserDetail> GetUsers()
        {
            if (users == null) users = GetAllUsers();

            return users;
        }
    }
}
