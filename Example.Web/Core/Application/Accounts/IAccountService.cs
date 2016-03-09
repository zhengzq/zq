﻿using Zq;

namespace Example.Web.Core.Application.Accounts
{

    public interface IAccountService
    {
        OperateResult GetAccountByLoginName(string loginName);
        bool ValidateLoginNameWithPassword(string loginName, string password);
    }
  
}