using BankManagement.Enums;
using BankManagement.Model;

namespace BankManagement.Service;

public class AccountService
{
    private readonly CustomerService _customerService;
    private readonly TransactionService _transactionService;
    private readonly List<Account> _accounts = new ();
    private int _accountId = 1;

    public AccountService(CustomerService customerService, TransactionService transactionService)
    {
        _customerService = customerService;
        _transactionService = transactionService;
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
        
        if (account is null)
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
        _transactionService.AddTransaction(accountId, TransactionType.Deposit, amount);
        return true;
    }

    public bool Withdraw(int accountId, decimal amount)
    {
        var account = GetAccountById(accountId);
        if (account is null || amount <= 0 || amount > account.Balance)
            return false;

        account.Balance -= amount;
        _transactionService.AddTransaction(accountId, TransactionType.Withdraw, amount);
        return true;
    }

    public bool Transfer(int fromAccountId, int toAccountId, decimal amount)
    {
        var fromAccount = GetAccountById(fromAccountId);
        var toAccount = GetAccountById(toAccountId);

        if (fromAccount is null || toAccount is null || amount <= 0 || amount > fromAccount.Balance || fromAccount == toAccount)
            return false;

        fromAccount.Balance -= amount;
        toAccount.Balance += amount;
        _transactionService.AddTransaction(fromAccountId, TransactionType.Transfer, amount);
        _transactionService.AddTransaction(toAccountId, TransactionType.Deposit, amount);
        return true;
    }
}