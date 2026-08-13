using BankManagement.Enums;
using BankManagement.Model;

namespace BankManagement.Service;

public class AccountService
{
    private readonly CustomerService _customerService;
    private readonly List<Account> _accounts = new ();
    private int _accountId = 1;

    public AccountService(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public bool AddAccount(int customerId, AccountType accountType)
    {
        if (_customerService.GetCustomerById(customerId) is null)
            return false;
        
        _accounts.Add(new Account
        {
            AccountId = _accountId++,
            CustomerId = customerId,
            AccountType = accountType,
            Balance = 0,
        });
        return true;
    }

    public List<Account> GetAccounts()
    {
        return _accounts.ToList();
    }

    public Account? GetAccountById(int accountId)
    {
        return _accounts.FirstOrDefault(x => x.AccountId == accountId);
    }

    public List<Account> GetAccountByCustomerId(int customerId)
    {
        return _accounts.Where(x => x.CustomerId == customerId).ToList();
    }

    public bool DeleteAccount(int accountId)
    {
        var account = GetAccountById(accountId);
        
        if (account == null)
            return false;
        
        _accounts.Remove(account);
        return true;
    }
    
    public bool Deposit(int accountId, decimal amount)
    {
        var account = GetAccountById(accountId);
        if (account is null || amount <= 0)
            return false;

        account.Balance += amount;
        return true;
    }
}