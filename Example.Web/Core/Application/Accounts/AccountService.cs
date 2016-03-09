
using Example.Web.Core.Domain.Accounts;
using Zq;
using Zq.Domain;
using Zq.Ioc;

namespace Example.Web.Core.Application.Accounts
{
    [Component(typeof(IAccountService))]
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public OperateResult GetAccountByLoginName(string loginName)
        {
            try
            {
                var account = _accountRepository.GetAccountByLoginName(loginName);

                account.CheckAccount();
                return new OperateResult(ResultState.Success, "", 0, account);
            }
            catch (DomainException dex)
            {
                return new OperateResult(ResultState.Error, dex.Message, dex.Code);
            }

        }

        public bool ValidateLoginNameWithPassword(string loginName, string password)
        {
            return _accountRepository.Validate(loginName, password);
        }
    }
}