using BankManagement.Enums;
using BankManagement.Service;

namespace BankManagement.Tests;

public class AccountServiceTests
{
    [Fact]
    public void Deposit_ShouldDepositAmount()
    {
        // Arrange
        CustomerService customerService = new CustomerService();
        AccountService accountService = new AccountService(customerService);
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
        AccountService accountService = new AccountService(customerService);
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
        AccountService accountService = new AccountService(customerService);
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
    public void Withdraw_ShouldWithdrawAmount()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        AccountService accountService = new AccountService(customerService);
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
    public void Withdraw_ShouldReturnFalse_WhenAccountDoesNotExist()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        AccountService accountService = new AccountService(customerService);
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
        AccountService accountService = new AccountService(customerService);
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
        AccountService accountService = new AccountService(customerService);
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
        AccountService accountService = new AccountService(customerService);
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
    public void Transfer_ShouldReturnFalse_WhenFromAccountDoesNotExist()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        AccountService accountService = new AccountService(customerService);
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
        AccountService accountService = new AccountService(customerService);
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
        AccountService accountService = new AccountService(customerService);
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
        AccountService accountService = new AccountService(customerService);
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