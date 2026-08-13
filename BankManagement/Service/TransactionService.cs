using BankManagement.Enums;
using BankManagement.Model;

namespace BankManagement.Service;

public class TransactionService
{
    public readonly List<Transaction> _transactions = new ();
    private readonly AccountService _accountService;
    private int _transactionId = 1;

    public TransactionService(AccountService accountService)
    {
        _accountService = accountService;
    }

    public void AddTransaction(int accountId, TransactionType transactionType, decimal amount)
    {
        _transactions.Add(new Transaction()
        {
            TransactionId = _transactionId++,
            AccountId = accountId,
            Amount = amount,
            TransactionType = transactionType,
            Date = DateTime.Now
        });
    }

    public List<Transaction> GetTransactions()
    {
        return _transactions.ToList();
    }

    public Transaction? GetTransactionById(int transactionId)
    {
        return _transactions.FirstOrDefault(x => x.TransactionId == transactionId);
    }
}