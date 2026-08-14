using BankManagement.Service;
using BankManagement.UI;

CustomerService customerService = new CustomerService();
TransactionService transactionService = new TransactionService();
AccountService  accountService = new AccountService(customerService, transactionService);

BankMenu bankMenu = new BankMenu(customerService, transactionService, accountService);
bankMenu.ShowMenu();