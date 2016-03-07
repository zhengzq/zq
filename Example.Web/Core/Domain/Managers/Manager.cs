using System;
using Zq.Domain;

namespace Example.Web.Core.Domain.Managers
{
    public class Manager : AggregateRoot<int>
    {
        public Manager()
        {
        }

        public Manager(int id
            , string loginName
            , string userName
            , string password
            , int roleId
            , string cellphone
            , string email)
        {
            base.Id = id;
            this.LoginName = loginName;
            this.UserName = userName;
            this.Password = password;
            this.RoleId = roleId;
            this.Cellphone = cellphone;
            this.Email = email;
            this.IsSys = false;
            this.CreatedTime = DateTime.Now;
        }

        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public bool IsEnable { get; set; }
        public bool IsSys { get; set; }
        public DateTime CreatedTime { get; set; }
        public void UpdatePassword(string password, string repeatPassword)
        {
            if (!password.Equals(repeatPassword))
                throw new DomainException("登录名已经存在", 100);
            if (!this.Password.Equals(password))
                throw new DomainException("旧密码不正确", 100);
            this.Password = password;
        }

        public void SetIsEnable(bool value)
        {
            this.IsEnable = value;
        }

        public void CheckIsSys()
        {
            if (this.IsSys)
                throw new DomainException("系统管理员不能被删除");
        }
    }
}
