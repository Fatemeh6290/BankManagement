using BankManagement.Enums;
using BankManagement.Service;

namespace BankManagement.Tests;

public class TransactionServiceTests
{
    [Fact]
    public void AddTransactionTest_ShouldAddTransaction()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        AccountService accountService = new AccountService(customerService);
        TransactionService transactionService = new TransactionService(accountService);

        //Act
        transactionService.AddTransaction(1, TransactionType.Deposit, 300);
        var result = transactionService.GetTransactionById(1);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(300, result.Amount);
        Assert.Equal(TransactionType.Deposit, result.TransactionType);
    }
    
    [Fact]
    public void GetTransactionsTest_ShouldReturnTransactions()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        AccountService accountService = new AccountService(customerService);
        TransactionService transactionService = new TransactionService(accountService);

        //Act
        transactionService.AddTransaction(1, TransactionType.Deposit, 300);
        transactionService.AddTransaction(1, TransactionType.Deposit, 100);
        transactionService.AddTransaction(1, TransactionType.Transfer, 500);
        transactionService.AddTransaction(1, TransactionType.Withdraw, 3200);
        var result = transactionService.GetTransactions();
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
    }
    
    [Fact]
    public void GetTransactionByIdTest_ShouldReturnTransaction()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        AccountService accountService = new AccountService(customerService);
        TransactionService transactionService = new TransactionService(accountService);

        //Act
        transactionService.AddTransaction(1, TransactionType.Deposit, 300);
        var result = transactionService.GetTransactionById(1);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.TransactionId);
        Assert.Equal(1, result.AccountId);
        Assert.Equal(300, result.Amount);
        Assert.Equal(TransactionType.Deposit, result.TransactionType);
    }
    
    [Fact]
    public void GetTransactionById_ShouldReturnNull_WhenTransactionDoesNotExist()
    {
        //Arrange
        CustomerService customerService = new CustomerService();
        AccountService accountService = new AccountService(customerService);
        TransactionService transactionService = new TransactionService(accountService);

        //Act
        transactionService.AddTransaction(1, TransactionType.Deposit, 300);
        var result = transactionService.GetTransactionById(2);
        
        //Assert
        Assert.Null(result);
    }
}