using BankManagement.Enums;
using BankManagement.Service;

namespace BankManagement.Tests;

public class AccountServiceTests
{
    [Fact]
    public void AddAccount_ShouldAddAccount()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        
        //Act
        var result = accountService.AddAccount(1, AccountType.Checking);
        var account = accountService.GetAccountById(1);
        
        //Assert
        Assert.True(result);
        Assert.NotNull(account);
        Assert.Equal(AccountType.Checking, account.AccountType);
    }
    
    [Fact]
    public void AddAccount_ShouldReturnFalse_WhenCustomerDoesNotExist()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        
        //Act
        var result = accountService.AddAccount(2, AccountType.Checking);

        
        //Assert
        Assert.False(result);
    }
    
    [Fact]
    public void GetAccountById_ShouldReturnAccount()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        
        //Act
        var account = accountService.GetAccountById(1);
        
        //Assert
        Assert.NotNull(account);
        Assert.Equal(1, account.AccountId);
    }
    
    
    [Fact]
    public void GetAccountById_ShouldReturnNull_WhenAccountDoesNotExist()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        
        //Act
        var result = accountService.GetAccountById(2);
        
        //Assert
        Assert.Null(result);
    }
    
    [Fact]
    public void GetAccountByCustomerId_ShouldReturnAccounts()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        accountService.AddAccount(1, AccountType.Savings);
        
        //Act
        var accounts = accountService.GetAccountByCustomerId(1);
        
        //Assert
        Assert.Equal(2, accounts.Count);
        Assert.All(accounts, account => Assert.Equal(1, account.CustomerId));
    }
    
    [Fact]
    public void GetAccountByCustomerId_ShouldReturnEmptyList_WhenCustomerHasNoAccounts()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        
        //Act
        var account = accountService.GetAccountByCustomerId(1);
        
        //Assert
        Assert.Empty(account);
    }
    
    [Fact]
    public void GetAccounts_ShouldReturnAccounts()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        customerService.AddCustomer("Tim", "tim@gmail.com");
        accountService.AddAccount(2, AccountType.Checking);
        
        //Act
        var result = accountService.GetAccounts();
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public void DeleteAccount_ShouldDeleteAccount()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        
        //Act
        var result = accountService.DeleteAccount(1);
        var account = accountService.GetAccounts();
        
        //Assert
        Assert.True(result);
        Assert.Empty(account);
    }
    
    [Fact]
    public void DeleteAccount_ShouldReturnFalse_WhenAccountDoesNotExist()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        
        //Act
        var result = accountService.DeleteAccount(2);
        
        //Assert
        Assert.False(result);
    }
    
    [Fact]
    public void Deposit_ShouldDepositAmount()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        
        //Act
        var result = accountService.Deposit(1, 500);
        var account = accountService.GetAccountById(1);
        
        //Assert
        Assert.True(result);
        Assert.NotNull(account);
        Assert.Equal(500, account.Balance);
    }
    
    [Fact]
    public void Deposit_ShouldReturnFalse_WhenAccountDoesNotExist()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        
        //Act
        var result = accountService.Deposit(2, 500);
        
        //Assert
        Assert.False(result);
    }
    
    [Fact]
    public void Deposit_ShouldReturnFalse_WhenAmountIsInvalid()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        
        //Act
        var result1 = accountService.Deposit(1, 0);
        var result2 = accountService.Deposit(1, -500);


        //Assert
        Assert.False(result1);
        Assert.False(result2);
    }
    
    [Fact]
    public void Deposit_ShouldCreateTransaction()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        
        //Act
        var result = accountService.Deposit(1, 500);
        var transactions = transactionService.GetTransactions();
        
        //Assert
        Assert.True(result);
        Assert.Single(transactions);

        var transaction = transactions.First();

        Assert.Equal(1, transaction.AccountId);
        Assert.Equal(500, transaction.Amount);
        Assert.Equal(TransactionType.Deposit, transaction.TransactionType);
    }

    [Fact]
    public void Withdraw_ShouldWithdrawAmount()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter","peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        
        //Act
        var depositResult = accountService.Deposit(1, 1000);
        var result = accountService.Withdraw(1, 500);
        var account = accountService.GetAccountById(1);

        //Assert
        Assert.True(depositResult);
        Assert.True(result);
        Assert.NotNull(account);
        Assert.Equal(500, account.Balance);
    }
    
    [Fact]
    public void Withdraw_ShouldCreateTransaction()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        
        //Act
        accountService.Deposit(1, 500);
        var result = accountService.Withdraw(1, 200);
        var resultTransaction = transactionService.GetTransactions();
        
        //Assert
        Assert.True(result);
        Assert.NotNull(resultTransaction);
        Assert.Equal(2, resultTransaction.Count);
        Assert.Equal(200, resultTransaction[1].Amount);
    }
    
    [Fact]
    public void Withdraw_ShouldReturnFalse_WhenAccountDoesNotExist()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter","peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        
        //Act
        accountService.Deposit(1, 1000);
        var result = accountService.Withdraw(2, 500);
        var account = accountService.GetAccountById(2);
        
        //Assert
        Assert.False(result);
        Assert.Null(account);
    }
    
    [Fact]
    public void Withdraw_ShouldReturnFalse_WhenAmountIsInvalid()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter","peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        
        //Act
        accountService.Deposit(1, 499);
        var result = accountService.Withdraw(1, -100);
        
        //Assert
        Assert.False(result);
    }
    
    [Fact]
    public void Withdraw_ShouldReturnFalse_WhenBalanceIsInsufficient()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter","peter@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        
        //Act
        accountService.Deposit(1, 499);
        var result = accountService.Withdraw(1, 500);
        
        //Assert
        Assert.False(result);
    }

    [Fact]
    public void Transfer_ShouldTransferAmount()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        customerService.AddCustomer("Tim", "tim@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        accountService.AddAccount(2, AccountType.Checking);
        accountService.Deposit(1, 500);
        accountService.Deposit(2, 1000);

        //Act
        
        var result = accountService.Transfer(1,2, 200);

        //Assert
        Assert.True(result);
        Assert.NotNull(accountService.GetAccountById(2));
        Assert.NotNull(accountService.GetAccountById(1));
        Assert.Equal(300, accountService.GetAccountById(1)!.Balance);
        Assert.Equal(1200, accountService.GetAccountById(2)!.Balance);

    }
    
    [Fact]
    public void Transfer_ShouldCreateTransaction()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        customerService.AddCustomer("Tim", "tim@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        accountService.AddAccount(2, AccountType.Checking);
        accountService.Deposit(1, 500);
        accountService.Deposit(2, 1000);
        
        //Act
        var result = accountService.Transfer(1,2, 200);
        var resultTransaction = transactionService.GetTransactions();
        
        //Assert
        Assert.True(result);
        Assert.NotNull(resultTransaction);
        Assert.Equal(4, resultTransaction.Count);
        Assert.Equal(500, resultTransaction[0].Amount);
        Assert.Equal(1000, resultTransaction[1].Amount);
        Assert.Equal(200, resultTransaction[2].Amount);
        Assert.Equal(200, resultTransaction[3].Amount);
        Assert.Equal(1, resultTransaction[2].AccountId);
        Assert.Equal(2, resultTransaction[3].AccountId);
        Assert.Equal(TransactionType.Transfer, resultTransaction[2].TransactionType);
        Assert.Equal(TransactionType.Deposit, resultTransaction[3].TransactionType);
    }

    [Fact]
    public void Transfer_ShouldReturnFalse_WhenFromAccountDoesNotExist()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        customerService.AddCustomer("Tim", "tim@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        accountService.AddAccount(2, AccountType.Checking);
        accountService.Deposit(1, 500);
        accountService.Deposit(2, 1000);

        //Act
        
        var result = accountService.Transfer(3,1, 200);


        //Assert
        Assert.False(result);
    }
    
    [Fact]
    public void Transfer_ShouldReturnFalse_WhenToAccountDoesNotExist()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        customerService.AddCustomer("Tim", "tim@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        accountService.AddAccount(2, AccountType.Checking);
        accountService.Deposit(1, 500);
        accountService.Deposit(2, 1000);

        //Act
        
        var result = accountService.Transfer(1,3, 200);

        //Assert
        Assert.False(result);
    }
    
    [Fact]
    public void Transfer_ShouldReturnFalse_WhenAmountIsInvalid()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        customerService.AddCustomer("Tim", "tim@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        accountService.AddAccount(2, AccountType.Checking);
        accountService.Deposit(1, 500);
        accountService.Deposit(2, 1000);

        //Act
        
        var result = accountService.Transfer(1,2, -100);

        //Assert
        Assert.False(result);
    }
    
    [Fact]
    public void Transfer_ShouldReturnFalse_WhenBalanceIsInsufficient()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        TransactionService transactionService = new TransactionService();
        AccountService accountService = new AccountService(customerService, transactionService);
        customerService.AddCustomer("Peter", "peter@gmail.com");
        customerService.AddCustomer("Tim", "tim@gmail.com");
        accountService.AddAccount(1, AccountType.Checking);
        accountService.AddAccount(2, AccountType.Checking);
        accountService.Deposit(1, 500);
        accountService.Deposit(2, 1000);

        //Act
        
        var result = accountService.Transfer(1,2, 600);

        //Assert
        Assert.False(result);
    }
}