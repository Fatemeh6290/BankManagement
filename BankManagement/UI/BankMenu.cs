using BankManagement.Enums;
using BankManagement.Service;

namespace BankManagement.UI;

public class BankMenu
{
    private readonly CustomerService _customerService;
    private readonly TransactionService _transactionService;
    private readonly AccountService _accountService;
    
    public BankMenu(CustomerService customerService, TransactionService transactionService, AccountService accountService)
    {
        _customerService = customerService;
        _transactionService = transactionService;
        _accountService = accountService;
    }
    
    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("===== Bank Management =====");
            Console.WriteLine("1 - Customer Management");
            Console.WriteLine("2 - Account Management");
            Console.WriteLine("3 - Transaction");
            Console.WriteLine("0 - Exit");

            if (!int.TryParse(Console.ReadLine(), out int input))
            {
                Console.WriteLine("Please enter a valid option."); 
                continue;
            }

            switch (input)
            {
                case 0:
                    return;
                case 1:
                    CustomerManagement();
                    break;
                case 2:
                    AccountManagement();
                    break;
                case 3:
                    TransactionManagement();
                    break;
                default:
                    Console.WriteLine("Please enter a valid option.");
                    break;
            }
        }
    }

    private void CustomerManagement()
    {
        while (true)
        {
            Console.WriteLine("===== Customer Management =====");
            Console.WriteLine("1 - Add Customer");
            Console.WriteLine("2 - Show Customer");
            Console.WriteLine("3 - Search Customer By Id");
            Console.WriteLine("4 - Search Customer By Name");
            Console.WriteLine("5 - Delete Customer");
            Console.WriteLine("0 - Back");

            if (!int.TryParse(Console.ReadLine(), out int input))
            {
                Console.WriteLine("Please enter a valid option.");
                continue;
            }

            switch (input)
            {
                case 0:
                    return;
                case 1:
                    AddCustomer();
                    break;
                case 2:
                    ShowCustomer();
                    break;
                case 3:
                    SearchCustomerById();
                    break;
                case 4:
                    SearchCustomerByName();
                    break;
                case 5:
                    DeleteCustomer();
                    break;
                default:
                    Console.WriteLine("Please enter a valid option.");
                    break;
            }
        }
    }

    private void AddCustomer()
    {
        Console.WriteLine("Enter your name:");
        var name = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Please enter a valid name.");
            return;
        }
        
        Console.WriteLine("Enter your email:");
        var email = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            Console.WriteLine("Please enter a valid email.");
            return;
        }
        
        _customerService.AddCustomer(name, email);
        Console.WriteLine("Customer added successfully.");
    }
    
    private void ShowCustomer()
    {
        Console.WriteLine("Customer list:");
        var customers = _customerService.GetCustomers();

        if (customers.Count == 0)
        {
            Console.WriteLine("No customer found.");
            return;
        }

        foreach (var customer in customers)
        {
            Console.WriteLine($"Customer Id:{customer.CustomerId} - Name:{customer.Name} - Email:{customer.Email}");
        }        
    }
    
    private void SearchCustomerById()
    {
        int customerId = ReadPositiveInt("Enter Customer Id:");
        
        var customer = _customerService.GetCustomerById(customerId);

        if (customer == null)
        {
            Console.WriteLine("No customer found.");
            return;
        }
        
        Console.WriteLine($"Customer Id:{customer.CustomerId} - Name:{customer.Name} - Email:{customer.Email}");
    }
    
    private void SearchCustomerByName()
    {
        Console.WriteLine("Enter your customer name:");
        var name = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Please enter a valid name.");
            return;
        }
        
        var customers = _customerService.SearchCustomerByName(name);

        if (customers.Count == 0)
        {
            Console.WriteLine("No customer found.");
            return;
        }
        
        foreach (var customer in customers)
        {
            Console.WriteLine($"Customer Id:{customer.CustomerId} - Name:{customer.Name} - Email:{customer.Email}");
        }   
    }
    
    private void DeleteCustomer()
    {
        int customerId = ReadPositiveInt("Enter Customer Id:");
        
        
        var result = _customerService.DeleteCustomer(customerId);

        if (!result)
        {
            Console.WriteLine("Delete not successful.");
            return;
        }  
        
        Console.WriteLine("Customer delete successfully.");
    }

    private void AccountManagement()
    {
        while (true)
        {
            Console.WriteLine("===== Account Management =====");
            Console.WriteLine("1 - Add Account");
            Console.WriteLine("2 - Show Account");
            Console.WriteLine("3 - Search Account");
            Console.WriteLine("4 - Delete Account");
            Console.WriteLine("5 - Deposit");
            Console.WriteLine("6 - Withdraw");
            Console.WriteLine("7 - Transfer");
            Console.WriteLine("0 - Back");

            if (!int.TryParse(Console.ReadLine(), out int input))
            {
                Console.WriteLine("Please enter a valid option.");
                continue;
            }

            switch (input)
            {
                case 0:
                    return;
                case 1:
                    AddAccount();
                    break;
                case 2:
                    ShowAccounts();
                    break;
                case 3:
                    SearchAccount();
                    break;
                case 4:
                    DeleteAccount();
                    break;
                case 5:
                    Deposit();
                    break;
                case 6:
                    Withdraw();
                    break;
                case 7:
                    Transfer();
                    break;
                default:
                    Console.WriteLine("Please enter a valid option.");
                    break;
            }
        }
    }
    
    private void AddAccount()
    {
        int customerId = ReadPositiveInt("Enter Customer Id:");
        
        Console.WriteLine("Enter your Account type:");
        Console.WriteLine("0 - Checking");
        Console.WriteLine("1 - Savings");
        int.TryParse(Console.ReadLine(), out int accountTypeInput);
        
        if (accountTypeInput != 0 && accountTypeInput != 1)
        {
            Console.WriteLine("Please enter a valid type.");
            return;
        }

        AccountType accountType = (AccountType)accountTypeInput;
        var result = _accountService.AddAccount(customerId, accountType);

        if (!result)
        {
            Console.WriteLine("Account not added successfully."); 
            return;
        }
        
        Console.WriteLine("Account added successfully.");
    }
    
    private void ShowAccounts()
    {
        Console.WriteLine("Account list:");
        var accounts = _accountService.GetAccounts();

        if (accounts.Count == 0)
        {
            Console.WriteLine("No account found.");
            return;
        }

        foreach (var account in accounts)
        {
            Console.WriteLine($"Account Id:{account.AccountId} - Customer Id:{account.CustomerId} - Account Type:{account.AccountType} - Balance:{account.Balance}");
        }
    }
    
    private void SearchAccount()
    {
        int accountId = ReadPositiveInt("Enter account ID:");
        
        var account = _accountService.GetAccountById(accountId);

        if (account == null)
        {
            Console.WriteLine("No account found.");
            return;
        }
        
        Console.WriteLine($"Account Id:{account.AccountId} - Customer Id:{account.CustomerId} - Account Type:{account.AccountType} - Balance:{account.Balance}");
    }
    
    private void DeleteAccount()
    {
        int accountId = ReadPositiveInt("Enter account ID:");
        
        var result = _accountService.DeleteAccount(accountId);

        if (!result)
        {
            Console.WriteLine("Delete not successful.");
            return;
        }
        
        Console.WriteLine("Account deleted successfully.");        
    }

    private void Deposit()
    {
        int accountId = ReadPositiveInt("Enter account ID:");
        decimal amount = ReadPositiveDecimal("Enter transfer amount:");
        
        var result = _accountService.Deposit(accountId, amount);

        if (!result)
        {
            Console.WriteLine("Deposit not successful.");
            return;
        }
        
        Console.WriteLine("Deposit successful.");       
    }

    private void Withdraw()
    {
        int accountId = ReadPositiveInt("Enter account ID:");
        decimal amount = ReadPositiveDecimal("Enter transfer amount:");
        
        var result = _accountService.Withdraw(accountId, amount);

        if (!result)
        {
            Console.WriteLine("Withdraw not successful.");
            return;
        }
        
        Console.WriteLine("Withdraw successful.");     
    }

    private void Transfer()
    {
        int fromAccountId = ReadPositiveInt("Enter from account ID:");
        int toAccountId = ReadPositiveInt("Enter to account ID:");
        decimal amount = ReadPositiveDecimal("Enter transfer amount:");
        
        var result = _accountService.Transfer(fromAccountId, toAccountId, amount);

        if (!result)
        {
            Console.WriteLine("Transfer not successful.");
            return;
        }
        
        Console.WriteLine("Transfer successful.");     
    }

    private void TransactionManagement()
    {
        while (true)
        {
            Console.WriteLine("===== Transaction =====");
            Console.WriteLine("1 - Show Transactions");
            Console.WriteLine("2 - Search Transaction By Id");
            Console.WriteLine("0 - Back");

            if (!int.TryParse(Console.ReadLine(), out int input))
            {
                Console.WriteLine("Please enter a valid option.");
                continue;
            }

            switch (input)
            {
                case 0:
                    return;
                case 1:
                    ShowTransaction();
                    break;
                case 2:
                    SearchTransaction();
                    break;
                default:
                    Console.WriteLine("Please enter a valid option.");
                    break;
            }
        }
    }
    
    private void ShowTransaction()
    {
        Console.WriteLine("Transaction list:");
        var transactions = _transactionService.GetTransactions();

        if (transactions.Count == 0)
        {
            Console.WriteLine("No transaction found.");
            return;
        }

        foreach (var transaction in transactions)
        {
            Console.WriteLine($"Transaction Id:{transaction.TransactionId} - Account Id:{transaction.AccountId}" +
                              $" - Transaction Type:{transaction.TransactionType} - Amount:{transaction.Amount} - Date:{transaction.Date}");
        }        
    }

    private void SearchTransaction()
    {
        int transactionId = ReadPositiveInt("Enter Transaktion Id:");
    
        var transaction = _transactionService.GetTransactionById(transactionId);

        if (transaction == null)
        {
            Console.WriteLine("No transaction found.");
            return;
        }
    
        Console.WriteLine($"Transaction Id:{transaction.TransactionId} - Account Id:{transaction.AccountId}" +
                          $" - Transaction Type:{transaction.TransactionType} - Amount:{transaction.Amount} - Date:{transaction.Date}");
    }

    private int ReadPositiveInt(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            
            if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
                return value;
            
            Console.WriteLine("Please enter a valid number");
        }
    }

    private decimal ReadPositiveDecimal(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            if (decimal.TryParse(Console.ReadLine(), out decimal value) && value > 0)
                return value;
            
            Console.WriteLine("Please enter a valid number");
        }
    }
}